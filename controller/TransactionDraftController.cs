using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionDraftController : ControllerBase
    {
        private readonly ITransactionDraftService _TransactionDraftService;

        public TransactionDraftController(ITransactionDraftService TransactionDraftService)
        {
            _TransactionDraftService = TransactionDraftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionDraftList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionDrafts = await _TransactionDraftService.GetTransactionDraftListByValue(offset, limit, val);

            if (TransactionDrafts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionDrafts in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionDrafts);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionDraftList(string TransactionDraft_name)
        {
            var TransactionDrafts = await _TransactionDraftService.GetTransactionDraftList(TransactionDraft_name);

            if (TransactionDrafts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionDraft found for uci: {TransactionDraft_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionDrafts);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionDraft(string TransactionDraft_name)
        {
            var TransactionDrafts = await _TransactionDraftService.GetTransactionDraft(TransactionDraft_name);

            if (TransactionDrafts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionDraft found for uci: {TransactionDraft_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionDrafts);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDraft>> AddTransactionDraft(TransactionDraft TransactionDraft)
        {
            var dbTransactionDraft = await _TransactionDraftService.AddTransactionDraft(TransactionDraft);

            if (dbTransactionDraft == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionDraft.TbTransactionDraftName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionDraft", new { uci = TransactionDraft.TbTransactionDraftName }, TransactionDraft);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionDraft(TransactionDraft TransactionDraft)
        {           
            TransactionDraft dbTransactionDraft = await _TransactionDraftService.UpdateTransactionDraft(TransactionDraft);

            if (dbTransactionDraft == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionDraft.TbTransactionDraftName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionDraft(TransactionDraft TransactionDraft)
        {            
            (bool status, string message) = await _TransactionDraftService.DeleteTransactionDraft(TransactionDraft);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionDraft);
        }
    }
}
