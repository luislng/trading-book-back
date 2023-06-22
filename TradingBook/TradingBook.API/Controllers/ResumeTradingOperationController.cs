using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradingBook.Model.ResumeTradingOperationDto;

namespace TradingBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeTradingOperationController : ControllerBase
    {
        private readonly ILogger<ResumeTradingOperationController> _logger;

        public ResumeTradingOperationController(ILogger<ResumeTradingOperationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Resumes the trading operations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResumeTradingDto), 200)] 
        public IActionResult ResumeTradingOperations()
        {
            return Ok(new ResumeTradingDto());
        }

        /// <summary>
        /// Adds to deposit.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        [HttpPatch("AddDeposit")]
        [ProducesResponseType(typeof(ResumeTradingDto), 200)]
        public IActionResult AddToDeposit([FromBody][Required] DepositDto depositUpdate)
        {
            return Ok(new ResumeTradingDto());
        }
       
    }
}