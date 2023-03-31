using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor4Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor4Service _ViewScheduleReferralVendor4Service;

        public ViewScheduleReferralVendor4Controller(IViewScheduleReferralVendor4Service ViewScheduleReferralVendor4Service)
        {
            _ViewScheduleReferralVendor4Service = ViewScheduleReferralVendor4Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor4List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor4s = await _ViewScheduleReferralVendor4Service.GetViewScheduleReferralVendor4ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor4s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor4s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor4s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor4List(string ViewScheduleReferralVendor4_name)
        {
            var ViewScheduleReferralVendor4s = await _ViewScheduleReferralVendor4Service.GetViewScheduleReferralVendor4List(ViewScheduleReferralVendor4_name);

            if (ViewScheduleReferralVendor4s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor4 found for uci: {ViewScheduleReferralVendor4_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor4s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor4(string ViewScheduleReferralVendor4_name)
        {
            var ViewScheduleReferralVendor4s = await _ViewScheduleReferralVendor4Service.GetViewScheduleReferralVendor4(ViewScheduleReferralVendor4_name);

            if (ViewScheduleReferralVendor4s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor4 found for uci: {ViewScheduleReferralVendor4_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor4s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor4>> AddViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {
            var dbViewScheduleReferralVendor4 = await _ViewScheduleReferralVendor4Service.AddViewScheduleReferralVendor4(ViewScheduleReferralVendor4);

            if (dbViewScheduleReferralVendor4 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor4.TbViewScheduleReferralVendor4Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor4", new { uci = ViewScheduleReferralVendor4.TbViewScheduleReferralVendor4Name }, ViewScheduleReferralVendor4);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {           
            ViewScheduleReferralVendor4 dbViewScheduleReferralVendor4 = await _ViewScheduleReferralVendor4Service.UpdateViewScheduleReferralVendor4(ViewScheduleReferralVendor4);

            if (dbViewScheduleReferralVendor4 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor4.TbViewScheduleReferralVendor4Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor4Service.DeleteViewScheduleReferralVendor4(ViewScheduleReferralVendor4);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor4);
        }
    }
}
