using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedTransactionController : ControllerBase
    {
        private readonly IDeletedTransactionService _DeletedTransactionService;

        public DeletedTransactionController(IDeletedTransactionService DeletedTransactionService)
        {
            _DeletedTransactionService = DeletedTransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTransactionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedTransactions = await _DeletedTransactionService.GetDeletedTransactionListByValue(offset, limit, val);

            if (DeletedTransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedTransactions in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTransactionList(string DeletedTransaction_name)
        {
            var DeletedTransactions = await _DeletedTransactionService.GetDeletedTransactionList(DeletedTransaction_name);

            if (DeletedTransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedTransaction found for uci: {DeletedTransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTransaction(string DeletedTransaction_name)
        {
            var DeletedTransactions = await _DeletedTransactionService.GetDeletedTransaction(DeletedTransaction_name);

            if (DeletedTransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedTransaction found for uci: {DeletedTransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTransactions);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedTransaction>> AddDeletedTransaction(DeletedTransaction DeletedTransaction)
        {
            var dbDeletedTransaction = await _DeletedTransactionService.AddDeletedTransaction(DeletedTransaction);

            if (dbDeletedTransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedTransaction.TbDeletedTransactionName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedTransaction", new { uci = DeletedTransaction.TbDeletedTransactionName }, DeletedTransaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedTransaction(DeletedTransaction DeletedTransaction)
        {           
            DeletedTransaction dbDeletedTransaction = await _DeletedTransactionService.UpdateDeletedTransaction(DeletedTransaction);

            if (dbDeletedTransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedTransaction.TbDeletedTransactionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedTransaction(DeletedTransaction DeletedTransaction)
        {            
            (bool status, string message) = await _DeletedTransactionService.DeleteDeletedTransaction(DeletedTransaction);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTransaction);
        }
    }
}
