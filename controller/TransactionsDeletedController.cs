using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsDeletedController : ControllerBase
    {
        private readonly ITransactionsDeletedService _TransactionsDeletedService;

        public TransactionsDeletedController(ITransactionsDeletedService TransactionsDeletedService)
        {
            _TransactionsDeletedService = TransactionsDeletedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeletedList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransactionsDeleteds = await _TransactionsDeletedService.GetTransactionsDeletedListByValue(offset, limit, val);

            if (TransactionsDeleteds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransactionsDeleteds in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteds);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeletedList(string TransactionsDeleted_name)
        {
            var TransactionsDeleteds = await _TransactionsDeletedService.GetTransactionsDeletedList(TransactionsDeleted_name);

            if (TransactionsDeleteds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleted found for uci: {TransactionsDeleted_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteds);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsDeleted(string TransactionsDeleted_name)
        {
            var TransactionsDeleteds = await _TransactionsDeletedService.GetTransactionsDeleted(TransactionsDeleted_name);

            if (TransactionsDeleteds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransactionsDeleted found for uci: {TransactionsDeleted_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleteds);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsDeleted>> AddTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {
            var dbTransactionsDeleted = await _TransactionsDeletedService.AddTransactionsDeleted(TransactionsDeleted);

            if (dbTransactionsDeleted == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleted.TbTransactionsDeletedName} could not be added."
                );
            }

            return CreatedAtAction("GetTransactionsDeleted", new { uci = TransactionsDeleted.TbTransactionsDeletedName }, TransactionsDeleted);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {           
            TransactionsDeleted dbTransactionsDeleted = await _TransactionsDeletedService.UpdateTransactionsDeleted(TransactionsDeleted);

            if (dbTransactionsDeleted == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransactionsDeleted.TbTransactionsDeletedName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {            
            (bool status, string message) = await _TransactionsDeletedService.DeleteTransactionsDeleted(TransactionsDeleted);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransactionsDeleted);
        }
    }
}
