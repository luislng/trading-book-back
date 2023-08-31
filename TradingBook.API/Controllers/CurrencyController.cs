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
        public async Task<IActionResult> GetAllCurrenciesAsync()
        {
            try
            {
                List<CurrencyDto> currencies = await _currencyService.GetCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpGet("CheckIfCurrencyCodeExists")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckIfCurrencyCodeExists([Required][FromQuery]string currencyCode)
        {
            return Ok(await _currencyService.CheckIfCurrencyCodeIsAvailable(currencyCode));   
        }

        [HttpPost]
        [ProducesResponseType(typeof(CurrencyDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCurrencyAsync(CurrencyDto currency)
        {
            try
            {
                CurrencyDto currencySaved = await _currencyService.SaveCurrencyAsync(currency);
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
        public async Task<IActionResult> DeleteCurrencyAsync(uint id)
        {
            try
            {
                await _currencyService.DeleteAsync(id);
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
        public async Task<IActionResult> ExchangeAsync([FromQuery][Required] decimal amount,
                                                  [FromQuery][Required] string currencyCodeFrom,
                                                  [FromQuery][Required] string currencyCodeTo)
        {
            try
            {
                ExchangeDto amountConverted = await _currencyService.ExchangeAsync(amount, currencyCodeFrom, currencyCodeTo);
                return Ok(amountConverted);  
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}