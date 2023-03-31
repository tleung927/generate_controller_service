using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsAll2Controller : ControllerBase
    {
        private readonly ITransactionsAll2Service _TransactionsAll2Service;

        public TransactionsAll2Controller(ITransactionsAll2Service TransactionsAll2Service)
        {
            _TransactionsAll2Service = TransactionsAll2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAll2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionsAll2s = await _TransactionsAll2Service.GetTransactionsAll2ListByValue(offset, limit, val);

            if (TransactionsAll2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionsAll2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAll2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAll2List(string TransactionsAll2_name)
        {
            var TransactionsAll2s = await _TransactionsAll2Service.GetTransactionsAll2List(TransactionsAll2_name);

            if (TransactionsAll2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsAll2 found for uci: {TransactionsAll2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAll2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAll2(string TransactionsAll2_name)
        {
            var TransactionsAll2s = await _TransactionsAll2Service.GetTransactionsAll2(TransactionsAll2_name);

            if (TransactionsAll2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsAll2 found for uci: {TransactionsAll2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAll2s);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsAll2>> AddTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {
            var dbTransactionsAll2 = await _TransactionsAll2Service.AddTransactionsAll2(TransactionsAll2);

            if (dbTransactionsAll2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsAll2.TbTransactionsAll2Name} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionsAll2", new { uci = TransactionsAll2.TbTransactionsAll2Name }, TransactionsAll2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {           
            TransactionsAll2 dbTransactionsAll2 = await _TransactionsAll2Service.UpdateTransactionsAll2(TransactionsAll2);

            if (dbTransactionsAll2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsAll2.TbTransactionsAll2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {            
            (bool status, string message) = await _TransactionsAll2Service.DeleteTransactionsAll2(TransactionsAll2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsAll2);
        }
    }
}
