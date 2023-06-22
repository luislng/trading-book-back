using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Model.Asset;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;

        public AssetController(ILogger<AssetController> logger)
        {
            _logger = logger;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(List<AssetDto>), 200)]
        public IActionResult GetAssets()
        {
            return Ok(new List<AssetDto>());
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<AssetDto>), StatusCodes.Status201Created)]
        public IActionResult SaveAsset([FromBody][Required]AssetDto asset)
        {
            return StatusCode(StatusCodes.Status201Created, asset);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteAsset([FromQuery][Required] string assetCode)
        {
            return NoContent();
        }

    }
}