using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CAllCaseLoadCountController : ControllerBase
    {
        private readonly ICAllCaseLoadCountService _CAllCaseLoadCountService;

        public CAllCaseLoadCountController(ICAllCaseLoadCountService CAllCaseLoadCountService)
        {
            _CAllCaseLoadCountService = CAllCaseLoadCountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCAllCaseLoadCountList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CAllCaseLoadCounts = await _CAllCaseLoadCountService.GetCAllCaseLoadCountListByValue(offset, limit, val);

            if (CAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CAllCaseLoadCounts in database");
            }

            return StatusCode(StatusCodes.Status200OK, CAllCaseLoadCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetCAllCaseLoadCountList(string CAllCaseLoadCount_name)
        {
            var CAllCaseLoadCounts = await _CAllCaseLoadCountService.GetCAllCaseLoadCountList(CAllCaseLoadCount_name);

            if (CAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CAllCaseLoadCount found for uci: {CAllCaseLoadCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CAllCaseLoadCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetCAllCaseLoadCount(string CAllCaseLoadCount_name)
        {
            var CAllCaseLoadCounts = await _CAllCaseLoadCountService.GetCAllCaseLoadCount(CAllCaseLoadCount_name);

            if (CAllCaseLoadCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CAllCaseLoadCount found for uci: {CAllCaseLoadCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CAllCaseLoadCounts);
        }

        [HttpPost]
        public async Task<ActionResult<CAllCaseLoadCount>> AddCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {
            var dbCAllCaseLoadCount = await _CAllCaseLoadCountService.AddCAllCaseLoadCount(CAllCaseLoadCount);

            if (dbCAllCaseLoadCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CAllCaseLoadCount.TbCAllCaseLoadCountName} could not be added."
                );
            }

            return CreatedAtAction("GetCAllCaseLoadCount", new { uci = CAllCaseLoadCount.TbCAllCaseLoadCountName }, CAllCaseLoadCount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {           
            CAllCaseLoadCount dbCAllCaseLoadCount = await _CAllCaseLoadCountService.UpdateCAllCaseLoadCount(CAllCaseLoadCount);

            if (dbCAllCaseLoadCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CAllCaseLoadCount.TbCAllCaseLoadCountName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {            
            (bool status, string message) = await _CAllCaseLoadCountService.DeleteCAllCaseLoadCount(CAllCaseLoadCount);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CAllCaseLoadCount);
        }
    }
}
