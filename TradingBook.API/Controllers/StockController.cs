using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.Stock.Abstract;
using TradingBook.Model.Invest;
using TradingBook.Model.Stock;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;

        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService; 
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<StockDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStocksAsync()
        {
            try
            {
                List<StockDto> stockDtos = await _stockService.GetAllAsync();
                return Ok(stockDtos);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockByIdAsync(uint id)
        {
            try
            {
                StockDto stockDtos = await _stockService.GetByIdAsync(id);
                return Ok(stockDtos);
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

        [HttpPost]
        [ProducesResponseType(typeof(uint), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveInvestAsync([FromBody][Required] SaveStockDto invest)
        {
            try
            {
                uint addedInvestId = await _stockService.SaveStockAsync(invest);  
                return StatusCode(StatusCodes.Status201Created, addedInvestId);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStockAsync(uint id)
        {
            try
            {
                await _stockService.DeleteAsync(id);
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
        public async Task<IActionResult> SellAsync([FromBody][Required] SellStockDto sellStock)
        {
            try
            {
                uint stockId = await _stockService.SellAsync(sellStock);
                return Ok(stockId);

            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, sellStock.StockId);
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
                uint stockId = await _stockService.UpdateMarketLimitAsync(marketLimits);
                return Ok(stockId);

            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, marketLimits.StockId);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}