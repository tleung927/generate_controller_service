using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RAllCaseLoadCountController : ControllerBase
    {
        private readonly IRAllCaseLoadCountService _RAllCaseLoadCountService;

        public RAllCaseLoadCountController(IRAllCaseLoadCountService RAllCaseLoadCountService)
        {
            _RAllCaseLoadCountService = RAllCaseLoadCountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRAllCaseLoadCountList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var RAllCaseLoadCounts = await _RAllCaseLoadCountService.GetRAllCaseLoadCountListByValue(offset, limit, val);

            if (RAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No RAllCaseLoadCounts in database");
            }

            return StatusCode(StatusCodes.Status200OK, RAllCaseLoadCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetRAllCaseLoadCountList(string RAllCaseLoadCount_name)
        {
            var RAllCaseLoadCounts = await _RAllCaseLoadCountService.GetRAllCaseLoadCountList(RAllCaseLoadCount_name);

            if (RAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RAllCaseLoadCount found for uci: {RAllCaseLoadCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RAllCaseLoadCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetRAllCaseLoadCount(string RAllCaseLoadCount_name)
        {
            var RAllCaseLoadCounts = await _RAllCaseLoadCountService.GetRAllCaseLoadCount(RAllCaseLoadCount_name);

            if (RAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RAllCaseLoadCount found for uci: {RAllCaseLoadCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RAllCaseLoadCounts);
        }

        [HttpPost]
        public async Task<ActionResult<RAllCaseLoadCount>> AddRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {
            var dbRAllCaseLoadCount = await _RAllCaseLoadCountService.AddRAllCaseLoadCount(RAllCaseLoadCount);

            if (dbRAllCaseLoadCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RAllCaseLoadCount.TbRAllCaseLoadCountName} could not be added."
                );
            }

            return CreatedAtAction("GetRAllCaseLoadCount", new { uci = RAllCaseLoadCount.TbRAllCaseLoadCountName }, RAllCaseLoadCount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {           
            RAllCaseLoadCount dbRAllCaseLoadCount = await _RAllCaseLoadCountService.UpdateRAllCaseLoadCount(RAllCaseLoadCount);

            if (dbRAllCaseLoadCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RAllCaseLoadCount.TbRAllCaseLoadCountName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {            
            (bool status, string message) = await _RAllCaseLoadCountService.DeleteRAllCaseLoadCount(RAllCaseLoadCount);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, RAllCaseLoadCount);
        }
    }
}
