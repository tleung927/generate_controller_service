using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor1Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor1Service _ViewScheduleReferralVendor1Service;

        public ViewScheduleReferralVendor1Controller(IViewScheduleReferralVendor1Service ViewScheduleReferralVendor1Service)
        {
            _ViewScheduleReferralVendor1Service = ViewScheduleReferralVendor1Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor1List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor1s = await _ViewScheduleReferralVendor1Service.GetViewScheduleReferralVendor1ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor1s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor1s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor1List(string ViewScheduleReferralVendor1_name)
        {
            var ViewScheduleReferralVendor1s = await _ViewScheduleReferralVendor1Service.GetViewScheduleReferralVendor1List(ViewScheduleReferralVendor1_name);

            if (ViewScheduleReferralVendor1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor1 found for uci: {ViewScheduleReferralVendor1_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor1s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor1(string ViewScheduleReferralVendor1_name)
        {
            var ViewScheduleReferralVendor1s = await _ViewScheduleReferralVendor1Service.GetViewScheduleReferralVendor1(ViewScheduleReferralVendor1_name);

            if (ViewScheduleReferralVendor1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor1 found for uci: {ViewScheduleReferralVendor1_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor1s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor1>> AddViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {
            var dbViewScheduleReferralVendor1 = await _ViewScheduleReferralVendor1Service.AddViewScheduleReferralVendor1(ViewScheduleReferralVendor1);

            if (dbViewScheduleReferralVendor1 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor1.TbViewScheduleReferralVendor1Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor1", new { uci = ViewScheduleReferralVendor1.TbViewScheduleReferralVendor1Name }, ViewScheduleReferralVendor1);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {           
            ViewScheduleReferralVendor1 dbViewScheduleReferralVendor1 = await _ViewScheduleReferralVendor1Service.UpdateViewScheduleReferralVendor1(ViewScheduleReferralVendor1);

            if (dbViewScheduleReferralVendor1 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor1.TbViewScheduleReferralVendor1Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor1Service.DeleteViewScheduleReferralVendor1(ViewScheduleReferralVendor1);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor1);
        }
    }
}
