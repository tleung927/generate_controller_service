using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsAllController : ControllerBase
    {
        private readonly ITransactionsAllService _TransactionsAllService;

        public TransactionsAllController(ITransactionsAllService TransactionsAllService)
        {
            _TransactionsAllService = TransactionsAllService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAllList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionsAlls = await _TransactionsAllService.GetTransactionsAllListByValue(offset, limit, val);

            if (TransactionsAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionsAlls in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAllList(string TransactionsAll_name)
        {
            var TransactionsAlls = await _TransactionsAllService.GetTransactionsAllList(TransactionsAll_name);

            if (TransactionsAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsAll found for uci: {TransactionsAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAll(string TransactionsAll_name)
        {
            var TransactionsAlls = await _TransactionsAllService.GetTransactionsAll(TransactionsAll_name);

            if (TransactionsAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsAll found for uci: {TransactionsAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAlls);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsAll>> AddTransactionsAll(TransactionsAll TransactionsAll)
        {
            var dbTransactionsAll = await _TransactionsAllService.AddTransactionsAll(TransactionsAll);

            if (dbTransactionsAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsAll.TbTransactionsAllName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionsAll", new { uci = TransactionsAll.TbTransactionsAllName }, TransactionsAll);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionsAll(TransactionsAll TransactionsAll)
        {           
            TransactionsAll dbTransactionsAll = await _TransactionsAllService.UpdateTransactionsAll(TransactionsAll);

            if (dbTransactionsAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsAll.TbTransactionsAllName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionsAll(TransactionsAll TransactionsAll)
        {            
            (bool status, string message) = await _TransactionsAllService.DeleteTransactionsAll(TransactionsAll);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAll);
        }
    }
}
