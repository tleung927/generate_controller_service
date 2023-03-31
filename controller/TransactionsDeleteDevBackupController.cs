using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsDeleteDevBackupController : ControllerBase
    {
        private readonly ITransactionsDeleteDevBackupService _TransactionsDeleteDevBackupService;

        public TransactionsDeleteDevBackupController(ITransactionsDeleteDevBackupService TransactionsDeleteDevBackupService)
        {
            _TransactionsDeleteDevBackupService = TransactionsDeleteDevBackupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionsDeleteDevBackups = await _TransactionsDeleteDevBackupService.GetTransactionsDeleteDevBackupListByValue(offset, limit, val);

            if (TransactionsDeleteDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionsDeleteDevBackups in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackupList(string TransactionsDeleteDevBackup_name)
        {
            var TransactionsDeleteDevBackups = await _TransactionsDeleteDevBackupService.GetTransactionsDeleteDevBackupList(TransactionsDeleteDevBackup_name);

            if (TransactionsDeleteDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleteDevBackup found for uci: {TransactionsDeleteDevBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleteDevBackup(string TransactionsDeleteDevBackup_name)
        {
            var TransactionsDeleteDevBackups = await _TransactionsDeleteDevBackupService.GetTransactionsDeleteDevBackup(TransactionsDeleteDevBackup_name);

            if (TransactionsDeleteDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleteDevBackup found for uci: {TransactionsDeleteDevBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackups);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsDeleteDevBackup>> AddTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {
            var dbTransactionsDeleteDevBackup = await _TransactionsDeleteDevBackupService.AddTransactionsDeleteDevBackup(TransactionsDeleteDevBackup);

            if (dbTransactionsDeleteDevBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleteDevBackup.TbTransactionsDeleteDevBackupName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionsDeleteDevBackup", new { uci = TransactionsDeleteDevBackup.TbTransactionsDeleteDevBackupName }, TransactionsDeleteDevBackup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {           
            TransactionsDeleteDevBackup dbTransactionsDeleteDevBackup = await _TransactionsDeleteDevBackupService.UpdateTransactionsDeleteDevBackup(TransactionsDeleteDevBackup);

            if (dbTransactionsDeleteDevBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleteDevBackup.TbTransactionsDeleteDevBackupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {            
            (bool status, string message) = await _TransactionsDeleteDevBackupService.DeleteTransactionsDeleteDevBackup(TransactionsDeleteDevBackup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteDevBackup);
        }
    }
}
