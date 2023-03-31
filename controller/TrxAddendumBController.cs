using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrxAddendumBController : ControllerBase
    {
        private readonly ITrxAddendumBService _TrxAddendumBService;

        public TrxAddendumBController(ITrxAddendumBService TrxAddendumBService)
        {
            _TrxAddendumBService = TrxAddendumBService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendumBList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TrxAddendumBs = await _TrxAddendumBService.GetTrxAddendumBListByValue(offset, limit, val);

            if (TrxAddendumBs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TrxAddendumBs in database");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendumBs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendumBList(string TrxAddendumB_name)
        {
            var TrxAddendumBs = await _TrxAddendumBService.GetTrxAddendumBList(TrxAddendumB_name);

            if (TrxAddendumBs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxAddendumB found for uci: {TrxAddendumB_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendumBs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxAddendumB(string TrxAddendumB_name)
        {
            var TrxAddendumBs = await _TrxAddendumBService.GetTrxAddendumB(TrxAddendumB_name);

            if (TrxAddendumBs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxAddendumB found for uci: {TrxAddendumB_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendumBs);
        }

        [HttpPost]
        public async Task<ActionResult<TrxAddendumB>> AddTrxAddendumB(TrxAddendumB TrxAddendumB)
        {
            var dbTrxAddendumB = await _TrxAddendumBService.AddTrxAddendumB(TrxAddendumB);

            if (dbTrxAddendumB == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxAddendumB.TbTrxAddendumBName} could not be added."
                );
            }

            return CreatedAtAction("GetTrxAddendumB", new { uci = TrxAddendumB.TbTrxAddendumBName }, TrxAddendumB);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrxAddendumB(TrxAddendumB TrxAddendumB)
        {           
            TrxAddendumB dbTrxAddendumB = await _TrxAddendumBService.UpdateTrxAddendumB(TrxAddendumB);

            if (dbTrxAddendumB == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxAddendumB.TbTrxAddendumBName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrxAddendumB(TrxAddendumB TrxAddendumB)
        {            
            (bool status, string message) = await _TrxAddendumBService.DeleteTrxAddendumB(TrxAddendumB);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TrxAddendumB);
        }
    }
}
