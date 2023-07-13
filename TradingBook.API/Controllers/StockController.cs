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
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                List<StockDto> stockDtos = await _stockService.GetAll();
                return Ok(stockDtos);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveInvest([FromBody][Required] SaveStockDto invest)
        {
            try
            {
                StockDto addedInvest = await _stockService.SaveStock(invest);  
                return StatusCode(StatusCodes.Status201Created, addedInvest);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteStock(uint id)
        {
            try
            {
                _stockService.Delete(id);
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
        [ProducesResponseType(typeof(StockDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Sell([FromBody][Required] SellStockDto sellStock)
        {
            try
            {
                StockDto invest = await _stockService.Sell(sellStock);
                return Ok(invest);

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
        [ProducesResponseType(typeof(StockDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMarketLimits([FromBody][Required]MarketLimitsDto marketLimits)
        {
            try
            {
                StockDto invest = await _stockService.SetMarketLimit(marketLimits);
                return Ok(invest);

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