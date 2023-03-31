using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedRtransactionController : ControllerBase
    {
        private readonly IDeletedRtransactionService _DeletedRtransactionService;

        public DeletedRtransactionController(IDeletedRtransactionService DeletedRtransactionService)
        {
            _DeletedRtransactionService = DeletedRtransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedRtransactionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedRtransactions = await _DeletedRtransactionService.GetDeletedRtransactionListByValue(offset, limit, val);

            if (DeletedRtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedRtransactions in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedRtransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedRtransactionList(string DeletedRtransaction_name)
        {
            var DeletedRtransactions = await _DeletedRtransactionService.GetDeletedRtransactionList(DeletedRtransaction_name);

            if (DeletedRtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedRtransaction found for uci: {DeletedRtransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedRtransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedRtransaction(string DeletedRtransaction_name)
        {
            var DeletedRtransactions = await _DeletedRtransactionService.GetDeletedRtransaction(DeletedRtransaction_name);

            if (DeletedRtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedRtransaction found for uci: {DeletedRtransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedRtransactions);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedRtransaction>> AddDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {
            var dbDeletedRtransaction = await _DeletedRtransactionService.AddDeletedRtransaction(DeletedRtransaction);

            if (dbDeletedRtransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedRtransaction.TbDeletedRtransactionName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedRtransaction", new { uci = DeletedRtransaction.TbDeletedRtransactionName }, DeletedRtransaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {           
            DeletedRtransaction dbDeletedRtransaction = await _DeletedRtransactionService.UpdateDeletedRtransaction(DeletedRtransaction);

            if (dbDeletedRtransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedRtransaction.TbDeletedRtransactionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {            
            (bool status, string message) = await _DeletedRtransactionService.DeleteDeletedRtransaction(DeletedRtransaction);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedRtransaction);
        }
    }
}
