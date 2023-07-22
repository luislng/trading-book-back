using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Application.Services.Deposit.Abstract;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositController : ControllerBase
    {
        private readonly ILogger<DepositController> _logger;
        private readonly IDepositService _depositService;

        public DepositController(ILogger<DepositController> logger, IDepositService DepositService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<DepositController>));
            _depositService = DepositService ?? throw new ArgumentNullException(nameof(IDepositService));
        }
                
        [HttpGet("TotalDeposit")]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TotalDepositAmount()
        {
            try
            {
                decimal amount = await _depositService.TotalDepositAmountAsync();   
                return Ok(amount);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }            
        }

        [HttpPost("AddDeposit")]
        [ProducesResponseType(typeof(string),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDepositAsync([FromQuery][Required]decimal amount)
        {
            try
            {
                await _depositService.AddDepositAsync(amount);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}