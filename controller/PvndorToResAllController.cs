using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PvndorToResAllController : ControllerBase
    {
        private readonly IPvndorToResAllService _PvndorToResAllService;

        public PvndorToResAllController(IPvndorToResAllService PvndorToResAllService)
        {
            _PvndorToResAllService = PvndorToResAllService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPvndorToResAllList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PvndorToResAlls = await _PvndorToResAllService.GetPvndorToResAllListByValue(offset, limit, val);

            if (PvndorToResAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PvndorToResAlls in database");
            }

            return StatusCode(StatusCodes.Status200OK, PvndorToResAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPvndorToResAllList(string PvndorToResAll_name)
        {
            var PvndorToResAlls = await _PvndorToResAllService.GetPvndorToResAllList(PvndorToResAll_name);

            if (PvndorToResAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PvndorToResAll found for uci: {PvndorToResAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PvndorToResAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPvndorToResAll(string PvndorToResAll_name)
        {
            var PvndorToResAlls = await _PvndorToResAllService.GetPvndorToResAll(PvndorToResAll_name);

            if (PvndorToResAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PvndorToResAll found for uci: {PvndorToResAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PvndorToResAlls);
        }

        [HttpPost]
        public async Task<ActionResult<PvndorToResAll>> AddPvndorToResAll(PvndorToResAll PvndorToResAll)
        {
            var dbPvndorToResAll = await _PvndorToResAllService.AddPvndorToResAll(PvndorToResAll);

            if (dbPvndorToResAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PvndorToResAll.TbPvndorToResAllName} could not be added."
                );
            }

            return CreatedAtAction("GetPvndorToResAll", new { uci = PvndorToResAll.TbPvndorToResAllName }, PvndorToResAll);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePvndorToResAll(PvndorToResAll PvndorToResAll)
        {           
            PvndorToResAll dbPvndorToResAll = await _PvndorToResAllService.UpdatePvndorToResAll(PvndorToResAll);

            if (dbPvndorToResAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PvndorToResAll.TbPvndorToResAllName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePvndorToResAll(PvndorToResAll PvndorToResAll)
        {            
            (bool status, string message) = await _PvndorToResAllService.DeletePvndorToResAll(PvndorToResAll);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PvndorToResAll);
        }
    }
}
