using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.Currency.Abstract;
using TradingBook.Model.Currency;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyService currencyService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CurrencyController>));
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(ICurrencyService));
        }
                
        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyDto>), 200)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCurrencies()
        {
            try
            {
                List<CurrencyDto> currencies = _currencyService.GetCurrencies();
                return Ok(currencies);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpPost]
        [ProducesResponseType(typeof(CurrencyDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult AddCurrency(CurrencyDto currency)
        {
            try
            {
                CurrencyDto currencySaved = _currencyService.SaveCurrency(currency);
                return Ok(currencySaved);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCurrency(uint id)
        {
            try
            {
                _currencyService.Delete(id);
                return Ok();

            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("Exchange")]
        [ProducesResponseType(typeof(ExchangeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Exchange([FromQuery][Required] decimal amount,
                                                  [FromQuery][Required] string currencyCodeFrom,
                                                  [FromQuery][Required] string currencyCodeTo)
        {
            try
            {
                ExchangeDto amountConverted = await _currencyService.Exchange(amount, currencyCodeFrom, currencyCodeTo);
                return Ok(amountConverted);  
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}