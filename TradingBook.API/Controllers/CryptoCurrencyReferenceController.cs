using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.CryptoCurrencyReference.Abstract;
using TradingBook.Model.CryptoCurrency;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoCurrencyReferenceController : ControllerBase
    {
        private readonly ILogger<CryptoCurrencyReferenceController> _logger;
        private readonly ICryptoCurrencyReferenceService _cryptoCurrencyRefService;

        public CryptoCurrencyReferenceController(ILogger<CryptoCurrencyReferenceController> logger, ICryptoCurrencyReferenceService cryptoRefService)
        {
            _logger = logger;
            _cryptoCurrencyRefService = cryptoRefService; 
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<CryptoCurrencyReferenceDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<CryptoCurrencyReferenceDto> cryptoRefDtos = await _cryptoCurrencyRefService.GetAllAsync();
                return Ok(cryptoRefDtos);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CryptoCurrencyReferenceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAsync([FromBody][Required] CryptoCurrencyReferenceDto cryptoRef)
        {
            try
            {
                CryptoCurrencyReferenceDto cryptoRefCreated = await _cryptoCurrencyRefService.SaveAsync(cryptoRef);  
                return StatusCode(StatusCodes.Status201Created, cryptoRefCreated);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(uint id)
        {
            try
            {
                await _cryptoCurrencyRefService.DeleteAsync(id);
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
    }
}