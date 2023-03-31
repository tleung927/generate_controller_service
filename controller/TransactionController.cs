using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _TransactionService;

        public TransactionController(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Transactions = await _TransactionService.GetTransactionListByValue(offset, limit, val);

            if (Transactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Transactions in database");
            }

            return StatusCode(StatusCodes.Status200OK, Transactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionList(string Transaction_name)
        {
            var Transactions = await _TransactionService.GetTransactionList(Transaction_name);

            if (Transactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Transaction found for uci: {Transaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Transactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction(string Transaction_name)
        {
            var Transactions = await _TransactionService.GetTransaction(Transaction_name);

            if (Transactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Transaction found for uci: {Transaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Transactions);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction(Transaction Transaction)
        {
            var dbTransaction = await _TransactionService.AddTransaction(Transaction);

            if (dbTransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Transaction.TbTransactionName} could not be added."
                );
            }

            return CreatedAtAction("GetTransaction", new { uci = Transaction.TbTransactionName }, Transaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(Transaction Transaction)
        {           
            Transaction dbTransaction = await _TransactionService.UpdateTransaction(Transaction);

            if (dbTransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Transaction.TbTransactionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction(Transaction Transaction)
        {            
            (bool status, string message) = await _TransactionService.DeleteTransaction(Transaction);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Transaction);
        }
    }
}
