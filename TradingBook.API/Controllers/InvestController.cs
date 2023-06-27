using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Model.Invest;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestController : ControllerBase
    {
        private readonly ILogger<InvestController> _logger;

        public InvestController(ILogger<InvestController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<InvestDto>),StatusCodes.Status200OK)]
        public IActionResult GetInvestCollection()
        {
            return Ok(new List<InvestDto>());
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddInvestDto), StatusCodes.Status201Created)]
        public IActionResult SaveInvest([FromBody][Required] AddInvestDto invest)
        {
            return StatusCode(StatusCodes.Status201Created, invest);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteAsset(uint id)
        {
            return NoContent();
        }

        [HttpPatch("{id}/Sell")]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        public IActionResult SellInvest(uint id,[FromBody][Required] SellInvestDto investSell)
        {
            return Ok();
        }

        [HttpPatch("{id}/MarketLimit")]
        public IActionResult UpdateMarketLimits([FromBody][Required]UpdateMarketLimitsDto marketLimits)
        {
            return Ok();
        }

    }
}