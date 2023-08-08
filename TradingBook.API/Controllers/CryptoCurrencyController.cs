using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.CryptoCurrency.Abstract;
using TradingBook.Model.CryptoCurrency;
using TradingBook.Model.Invest;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly ILogger<CryptoCurrencyController> _logger;
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public CryptoCurrencyController(ILogger<CryptoCurrencyController> logger, ICryptoCurrencyService cryptoService)
        {
            _logger = logger;
            _cryptoCurrencyService = cryptoService; 
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<CryptoDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCryptoCurrenciesAsync()
        {
            try
            {
                List<CryptoDto> cryptoCurrencyDtos = await _cryptoCurrencyService.GetAllAsync();
                return Ok(cryptoCurrencyDtos);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CryptoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCryptoCurrencyByIdAsync(uint id)
        {
            try
            {
                CryptoDto cryptoCurrencyDto = await _cryptoCurrencyService.GetByIdAsync(id);
                return Ok(cryptoCurrencyDto);
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("TotalEurEarned")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TotalEurEarned()
        {
            try
            {
                decimal total = await _cryptoCurrencyService.TotalEurEarnedAsync();                
                return Ok(new { Amount = total });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(uint), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveCryptoCurrencyAsync([FromBody][Required] SaveCryptoDto invest)
        {
            try
            {
                uint id = await _cryptoCurrencyService.SaveCryptoCurrencyAsync(invest);  
                return StatusCode(StatusCodes.Status201Created, id);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCryptoCurrencyAsync(uint id)
        {
            try
            {
                await _cryptoCurrencyService.DeleteAsync(id);
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

        [HttpPatch("Sell")]
        [ProducesResponseType(typeof(uint),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SellAsync([FromBody][Required] SellCryptoDto sellStock)
        {
            try
            {
                uint id = await _cryptoCurrencyService.SellAsync(sellStock);
                return Ok(id);

            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, sellStock.CryptoCurrencyId);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPatch("MarketLimit")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(uint), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMarketLimitsAsync([FromBody][Required]MarketLimitsDto marketLimits)
        {
            try
            {
                uint cryptoCurrencyId = await _cryptoCurrencyService.UpdateMarketLimitAsync(marketLimits);
                return Ok(cryptoCurrencyId);

            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, marketLimits.CryptoCurrencyId);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}