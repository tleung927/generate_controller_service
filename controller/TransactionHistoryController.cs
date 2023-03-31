using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _TransactionHistoryService;

        public TransactionHistoryController(ITransactionHistoryService TransactionHistoryService)
        {
            _TransactionHistoryService = TransactionHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionHistoryList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionHistorys = await _TransactionHistoryService.GetTransactionHistoryListByValue(offset, limit, val);

            if (TransactionHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionHistorys in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionHistoryList(string TransactionHistory_name)
        {
            var TransactionHistorys = await _TransactionHistoryService.GetTransactionHistoryList(TransactionHistory_name);

            if (TransactionHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionHistory found for uci: {TransactionHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionHistory(string TransactionHistory_name)
        {
            var TransactionHistorys = await _TransactionHistoryService.GetTransactionHistory(TransactionHistory_name);

            if (TransactionHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionHistory found for uci: {TransactionHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionHistorys);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionHistory>> AddTransactionHistory(TransactionHistory TransactionHistory)
        {
            var dbTransactionHistory = await _TransactionHistoryService.AddTransactionHistory(TransactionHistory);

            if (dbTransactionHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionHistory.TbTransactionHistoryName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionHistory", new { uci = TransactionHistory.TbTransactionHistoryName }, TransactionHistory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionHistory(TransactionHistory TransactionHistory)
        {           
            TransactionHistory dbTransactionHistory = await _TransactionHistoryService.UpdateTransactionHistory(TransactionHistory);

            if (dbTransactionHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionHistory.TbTransactionHistoryName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionHistory(TransactionHistory TransactionHistory)
        {            
            (bool status, string message) = await _TransactionHistoryService.DeleteTransactionHistory(TransactionHistory);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionHistory);
        }
    }
}
