using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RtransactionController : ControllerBase
    {
        private readonly IRtransactionService _RtransactionService;

        public RtransactionController(IRtransactionService RtransactionService)
        {
            _RtransactionService = RtransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRtransactionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Rtransactions = await _RtransactionService.GetRtransactionListByValue(offset, limit, val);

            if (Rtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Rtransactions in database");
            }

            return StatusCode(StatusCodes.Status200OK, Rtransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtransactionList(string Rtransaction_name)
        {
            var Rtransactions = await _RtransactionService.GetRtransactionList(Rtransaction_name);

            if (Rtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rtransaction found for uci: {Rtransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rtransactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtransaction(string Rtransaction_name)
        {
            var Rtransactions = await _RtransactionService.GetRtransaction(Rtransaction_name);

            if (Rtransactions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rtransaction found for uci: {Rtransaction_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rtransactions);
        }

        [HttpPost]
        public async Task<ActionResult<Rtransaction>> AddRtransaction(Rtransaction Rtransaction)
        {
            var dbRtransaction = await _RtransactionService.AddRtransaction(Rtransaction);

            if (dbRtransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rtransaction.TbRtransactionName} could not be added."
                );
            }

            return CreatedAtAction("GetRtransaction", new { uci = Rtransaction.TbRtransactionName }, Rtransaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRtransaction(Rtransaction Rtransaction)
        {           
            Rtransaction dbRtransaction = await _RtransactionService.UpdateRtransaction(Rtransaction);

            if (dbRtransaction == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rtransaction.TbRtransactionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRtransaction(Rtransaction Rtransaction)
        {            
            (bool status, string message) = await _RtransactionService.DeleteRtransaction(Rtransaction);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Rtransaction);
        }
    }
}
