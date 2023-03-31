using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrxAddendumController : ControllerBase
    {
        private readonly ITrxAddendumService _TrxAddendumService;

        public TrxAddendumController(ITrxAddendumService TrxAddendumService)
        {
            _TrxAddendumService = TrxAddendumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TrxAddendums = await _TrxAddendumService.GetTrxAddendumListByValue(offset, limit, val);

            if (TrxAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TrxAddendums in database");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendumList(string TrxAddendum_name)
        {
            var TrxAddendums = await _TrxAddendumService.GetTrxAddendumList(TrxAddendum_name);

            if (TrxAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxAddendum found for uci: {TrxAddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendum(string TrxAddendum_name)
        {
            var TrxAddendums = await _TrxAddendumService.GetTrxAddendum(TrxAddendum_name);

            if (TrxAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxAddendum found for uci: {TrxAddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendums);
        }

        [HttpPost]
        public async Task<ActionResult<TrxAddendum>> AddTrxAddendum(TrxAddendum TrxAddendum)
        {
            var dbTrxAddendum = await _TrxAddendumService.AddTrxAddendum(TrxAddendum);

            if (dbTrxAddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxAddendum.TbTrxAddendumName} could not be added."
                );
            }

            return CreatedAtAction("GetTrxAddendum", new { uci = TrxAddendum.TbTrxAddendumName }, TrxAddendum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrxAddendum(TrxAddendum TrxAddendum)
        {           
            TrxAddendum dbTrxAddendum = await _TrxAddendumService.UpdateTrxAddendum(TrxAddendum);

            if (dbTrxAddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxAddendum.TbTrxAddendumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrxAddendum(TrxAddendum TrxAddendum)
        {            
            (bool status, string message) = await _TrxAddendumService.DeleteTrxAddendum(TrxAddendum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendum);
        }
    }
}
