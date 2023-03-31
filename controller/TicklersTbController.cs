using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicklersTbController : ControllerBase
    {
        private readonly ITicklersTbService _TicklersTbService;

        public TicklersTbController(ITicklersTbService TicklersTbService)
        {
            _TicklersTbService = TicklersTbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTbList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TicklersTbs = await _TicklersTbService.GetTicklersTbListByValue(offset, limit, val);

            if (TicklersTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TicklersTbs in database");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTbs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTbList(string TicklersTb_name)
        {
            var TicklersTbs = await _TicklersTbService.GetTicklersTbList(TicklersTb_name);

            if (TicklersTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersTb found for uci: {TicklersTb_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTbs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTb(string TicklersTb_name)
        {
            var TicklersTbs = await _TicklersTbService.GetTicklersTb(TicklersTb_name);

            if (TicklersTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersTb found for uci: {TicklersTb_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTbs);
        }

        [HttpPost]
        public async Task<ActionResult<TicklersTb>> AddTicklersTb(TicklersTb TicklersTb)
        {
            var dbTicklersTb = await _TicklersTbService.AddTicklersTb(TicklersTb);

            if (dbTicklersTb == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersTb.TbTicklersTbName} could not be added."
                );
            }

            return CreatedAtAction("GetTicklersTb", new { uci = TicklersTb.TbTicklersTbName }, TicklersTb);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicklersTb(TicklersTb TicklersTb)
        {           
            TicklersTb dbTicklersTb = await _TicklersTbService.UpdateTicklersTb(TicklersTb);

            if (dbTicklersTb == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersTb.TbTicklersTbName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTicklersTb(TicklersTb TicklersTb)
        {            
            (bool status, string message) = await _TicklersTbService.DeleteTicklersTb(TicklersTb);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTb);
        }
    }
}
