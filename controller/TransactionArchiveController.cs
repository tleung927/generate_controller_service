using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionArchiveController : ControllerBase
    {
        private readonly ITransactionArchiveService _TransactionArchiveService;

        public TransactionArchiveController(ITransactionArchiveService TransactionArchiveService)
        {
            _TransactionArchiveService = TransactionArchiveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionArchiveList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionArchives = await _TransactionArchiveService.GetTransactionArchiveListByValue(offset, limit, val);

            if (TransactionArchives == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionArchives in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionArchives);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionArchiveList(string TransactionArchive_name)
        {
            var TransactionArchives = await _TransactionArchiveService.GetTransactionArchiveList(TransactionArchive_name);

            if (TransactionArchives == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionArchive found for uci: {TransactionArchive_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionArchives);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionArchive(string TransactionArchive_name)
        {
            var TransactionArchives = await _TransactionArchiveService.GetTransactionArchive(TransactionArchive_name);

            if (TransactionArchives == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionArchive found for uci: {TransactionArchive_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionArchives);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionArchive>> AddTransactionArchive(TransactionArchive TransactionArchive)
        {
            var dbTransactionArchive = await _TransactionArchiveService.AddTransactionArchive(TransactionArchive);

            if (dbTransactionArchive == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionArchive.TbTransactionArchiveName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionArchive", new { uci = TransactionArchive.TbTransactionArchiveName }, TransactionArchive);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionArchive(TransactionArchive TransactionArchive)
        {           
            TransactionArchive dbTransactionArchive = await _TransactionArchiveService.UpdateTransactionArchive(TransactionArchive);

            if (dbTransactionArchive == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionArchive.TbTransactionArchiveName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionArchive(TransactionArchive TransactionArchive)
        {            
            (bool status, string message) = await _TransactionArchiveService.DeleteTransactionArchive(TransactionArchive);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionArchive);
        }
    }
}
