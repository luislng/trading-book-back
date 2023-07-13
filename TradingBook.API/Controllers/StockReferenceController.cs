using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.StockReference.Abstract;
using TradingBook.Model.Stock;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockReferenceController : ControllerBase
    {
        private readonly ILogger<StockReferenceController> _logger;
        private readonly IStockReferenceService _assetService;

        public StockReferenceController(ILogger<StockReferenceController> logger, IStockReferenceService assetService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<StockReferenceController>));
            _assetService = assetService ?? throw new ArgumentNullException(nameof(IStockReferenceService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StockReferenceDto>), 200)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAssets()
        {
            try
            {
                return Ok(_assetService.GetAllAssets());

            }catch(Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<StockReferenceDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult SaveAsset([FromBody][Required]StockReferenceDto asset)
        {
            try
            {
                StockReferenceDto assetSaved = _assetService.SaveAsset(asset);
                return StatusCode(StatusCodes.Status201Created, assetSaved);

            }catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAsset(uint id)
        {
            try
            {
                _assetService.Delete(id);
                return Ok();

            }catch(KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            catch(Exception e)
            {
                return Problem(e.Message);  
            }
        }
    }
}