using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.Abstract;
using TradingBook.Model.Asset;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly IAssetService _assetService;

        public AssetController(ILogger<AssetController> logger, IAssetService assetService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<AssetController>));
            _assetService = assetService ?? throw new ArgumentNullException(nameof(IAssetService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AssetDto>), 200)]
        public IActionResult GetAssets()
        {
            try
            {
                return Ok(_assetService.GetAssets());

            }catch(Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<AssetDto>), StatusCodes.Status201Created)]
        public IActionResult SaveAsset([FromBody][Required]AssetDto asset)
        {
            try
            {
                AssetDto assetSaved = _assetService.SaveAsset(asset);
                return StatusCode(StatusCodes.Status201Created, assetSaved);

            }catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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