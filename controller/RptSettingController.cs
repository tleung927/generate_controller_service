using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RptSettingController : ControllerBase
    {
        private readonly IRptSettingService _RptSettingService;

        public RptSettingController(IRptSettingService RptSettingService)
        {
            _RptSettingService = RptSettingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRptSettingList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var RptSettings = await _RptSettingService.GetRptSettingListByValue(offset, limit, val);

            if (RptSettings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No RptSettings in database");
            }

            return StatusCode(StatusCodes.Status200OK, RptSettings);
        }

        [HttpGet]
        public async Task<IActionResult> GetRptSettingList(string RptSetting_name)
        {
            var RptSettings = await _RptSettingService.GetRptSettingList(RptSetting_name);

            if (RptSettings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RptSetting found for uci: {RptSetting_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RptSettings);
        }

        [HttpGet]
        public async Task<IActionResult> GetRptSetting(string RptSetting_name)
        {
            var RptSettings = await _RptSettingService.GetRptSetting(RptSetting_name);

            if (RptSettings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RptSetting found for uci: {RptSetting_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RptSettings);
        }

        [HttpPost]
        public async Task<ActionResult<RptSetting>> AddRptSetting(RptSetting RptSetting)
        {
            var dbRptSetting = await _RptSettingService.AddRptSetting(RptSetting);

            if (dbRptSetting == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RptSetting.TbRptSettingName} could not be added."
                );
            }

            return CreatedAtAction("GetRptSetting", new { uci = RptSetting.TbRptSettingName }, RptSetting);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRptSetting(RptSetting RptSetting)
        {           
            RptSetting dbRptSetting = await _RptSettingService.UpdateRptSetting(RptSetting);

            if (dbRptSetting == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RptSetting.TbRptSettingName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRptSetting(RptSetting RptSetting)
        {            
            (bool status, string message) = await _RptSettingService.DeleteRptSetting(RptSetting);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, RptSetting);
        }
    }
}
