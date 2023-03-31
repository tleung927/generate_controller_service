using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor2Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor2Service _ViewScheduleReferralVendor2Service;

        public ViewScheduleReferralVendor2Controller(IViewScheduleReferralVendor2Service ViewScheduleReferralVendor2Service)
        {
            _ViewScheduleReferralVendor2Service = ViewScheduleReferralVendor2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor2s = await _ViewScheduleReferralVendor2Service.GetViewScheduleReferralVendor2ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor2List(string ViewScheduleReferralVendor2_name)
        {
            var ViewScheduleReferralVendor2s = await _ViewScheduleReferralVendor2Service.GetViewScheduleReferralVendor2List(ViewScheduleReferralVendor2_name);

            if (ViewScheduleReferralVendor2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor2 found for uci: {ViewScheduleReferralVendor2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor2(string ViewScheduleReferralVendor2_name)
        {
            var ViewScheduleReferralVendor2s = await _ViewScheduleReferralVendor2Service.GetViewScheduleReferralVendor2(ViewScheduleReferralVendor2_name);

            if (ViewScheduleReferralVendor2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor2 found for uci: {ViewScheduleReferralVendor2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor2s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor2>> AddViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {
            var dbViewScheduleReferralVendor2 = await _ViewScheduleReferralVendor2Service.AddViewScheduleReferralVendor2(ViewScheduleReferralVendor2);

            if (dbViewScheduleReferralVendor2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor2.TbViewScheduleReferralVendor2Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor2", new { uci = ViewScheduleReferralVendor2.TbViewScheduleReferralVendor2Name }, ViewScheduleReferralVendor2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {           
            ViewScheduleReferralVendor2 dbViewScheduleReferralVendor2 = await _ViewScheduleReferralVendor2Service.UpdateViewScheduleReferralVendor2(ViewScheduleReferralVendor2);

            if (dbViewScheduleReferralVendor2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor2.TbViewScheduleReferralVendor2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor2Service.DeleteViewScheduleReferralVendor2(ViewScheduleReferralVendor2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor2);
        }
    }
}
