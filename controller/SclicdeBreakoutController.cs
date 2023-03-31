using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclicdeBreakoutController : ControllerBase
    {
        private readonly ISclicdeBreakoutService _SclicdeBreakoutService;

        public SclicdeBreakoutController(ISclicdeBreakoutService SclicdeBreakoutService)
        {
            _SclicdeBreakoutService = SclicdeBreakoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeBreakoutList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SclicdeBreakouts = await _SclicdeBreakoutService.GetSclicdeBreakoutListByValue(offset, limit, val);

            if (SclicdeBreakouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SclicdeBreakouts in database");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeBreakouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeBreakoutList(string SclicdeBreakout_name)
        {
            var SclicdeBreakouts = await _SclicdeBreakoutService.GetSclicdeBreakoutList(SclicdeBreakout_name);

            if (SclicdeBreakouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclicdeBreakout found for uci: {SclicdeBreakout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeBreakouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeBreakout(string SclicdeBreakout_name)
        {
            var SclicdeBreakouts = await _SclicdeBreakoutService.GetSclicdeBreakout(SclicdeBreakout_name);

            if (SclicdeBreakouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclicdeBreakout found for uci: {SclicdeBreakout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeBreakouts);
        }

        [HttpPost]
        public async Task<ActionResult<SclicdeBreakout>> AddSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {
            var dbSclicdeBreakout = await _SclicdeBreakoutService.AddSclicdeBreakout(SclicdeBreakout);

            if (dbSclicdeBreakout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclicdeBreakout.TbSclicdeBreakoutName} could not be added."
                );
            }

            return CreatedAtAction("GetSclicdeBreakout", new { uci = SclicdeBreakout.TbSclicdeBreakoutName }, SclicdeBreakout);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {           
            SclicdeBreakout dbSclicdeBreakout = await _SclicdeBreakoutService.UpdateSclicdeBreakout(SclicdeBreakout);

            if (dbSclicdeBreakout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclicdeBreakout.TbSclicdeBreakoutName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {            
            (bool status, string message) = await _SclicdeBreakoutService.DeleteSclicdeBreakout(SclicdeBreakout);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeBreakout);
        }
    }
}
