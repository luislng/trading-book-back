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
        public async Task<IActionResult> GetAllAssetsAsync()
        {
            try
            {
                return Ok(await _assetService.GetAllAssetsAsync());

            }catch(Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpGet("CheckIfStockExists")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckIfStockCodeExists([Required][FromQuery] string referenceCode)
        {
            return Ok(await _assetService.CheckIfStockExists(referenceCode));
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<StockReferenceDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAssetAsync([FromBody][Required]StockReferenceDto asset)
        {
            try
            {
                StockReferenceDto assetSaved = await _assetService.SaveAssetAsync(asset);
                return StatusCode(StatusCodes.Status201Created, assetSaved);

            }catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAssetAsync(uint id)
        {
            try
            {
                await _assetService.DeleteAsync(id);
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