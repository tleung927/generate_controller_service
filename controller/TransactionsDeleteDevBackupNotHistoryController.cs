using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsDeleteDevBackupNotHistoryController : ControllerBase
    {
        private readonly ITransactionsDeleteDevBackupNotHistoryService _TransactionsDeleteDevBackupNotHistoryService;

        public TransactionsDeleteDevBackupNotHistoryController(ITransactionsDeleteDevBackupNotHistoryService TransactionsDeleteDevBackupNotHistoryService)
        {
            _TransactionsDeleteDevBackupNotHistoryService = TransactionsDeleteDevBackupNotHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackupNotHistoryList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionsDeleteDevBackupNotHistorys = await _TransactionsDeleteDevBackupNotHistoryService.GetTransactionsDeleteDevBackupNotHistoryListByValue(offset, limit, val);

            if (TransactionsDeleteDevBackupNotHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionsDeleteDevBackupNotHistorys in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackupNotHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackupNotHistoryList(string TransactionsDeleteDevBackupNotHistory_name)
        {
            var TransactionsDeleteDevBackupNotHistorys = await _TransactionsDeleteDevBackupNotHistoryService.GetTransactionsDeleteDevBackupNotHistoryList(TransactionsDeleteDevBackupNotHistory_name);

            if (TransactionsDeleteDevBackupNotHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleteDevBackupNotHistory found for uci: {TransactionsDeleteDevBackupNotHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackupNotHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackupNotHistory(string TransactionsDeleteDevBackupNotHistory_name)
        {
            var TransactionsDeleteDevBackupNotHistorys = await _TransactionsDeleteDevBackupNotHistoryService.GetTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory_name);

            if (TransactionsDeleteDevBackupNotHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleteDevBackupNotHistory found for uci: {TransactionsDeleteDevBackupNotHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackupNotHistorys);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsDeleteDevBackupNotHistory>> AddTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {
            var dbTransactionsDeleteDevBackupNotHistory = await _TransactionsDeleteDevBackupNotHistoryService.AddTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory);

            if (dbTransactionsDeleteDevBackupNotHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleteDevBackupNotHistory.TbTransactionsDeleteDevBackupNotHistoryName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionsDeleteDevBackupNotHistory", new { uci = TransactionsDeleteDevBackupNotHistory.TbTransactionsDeleteDevBackupNotHistoryName }, TransactionsDeleteDevBackupNotHistory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {           
            TransactionsDeleteDevBackupNotHistory dbTransactionsDeleteDevBackupNotHistory = await _TransactionsDeleteDevBackupNotHistoryService.UpdateTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory);

            if (dbTransactionsDeleteDevBackupNotHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleteDevBackupNotHistory.TbTransactionsDeleteDevBackupNotHistoryName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {            
            (bool status, string message) = await _TransactionsDeleteDevBackupNotHistoryService.DeleteTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackupNotHistory);
        }
    }
}
