using Microsoft.AspNetCore.Mvc;
using TradingBook.Model.Currency;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }
                
        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyDto>), 200)] 
        public IActionResult GetCurrencies()
        {
            return Ok(new List<CurrencyDto>());
        }
    }
}