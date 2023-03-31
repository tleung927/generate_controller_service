using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor6Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor6Service _ViewScheduleReferralVendor6Service;

        public ViewScheduleReferralVendor6Controller(IViewScheduleReferralVendor6Service ViewScheduleReferralVendor6Service)
        {
            _ViewScheduleReferralVendor6Service = ViewScheduleReferralVendor6Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor6List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor6s = await _ViewScheduleReferralVendor6Service.GetViewScheduleReferralVendor6ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor6s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor6s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor6s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor6List(string ViewScheduleReferralVendor6_name)
        {
            var ViewScheduleReferralVendor6s = await _ViewScheduleReferralVendor6Service.GetViewScheduleReferralVendor6List(ViewScheduleReferralVendor6_name);

            if (ViewScheduleReferralVendor6s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor6 found for uci: {ViewScheduleReferralVendor6_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor6s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor6(string ViewScheduleReferralVendor6_name)
        {
            var ViewScheduleReferralVendor6s = await _ViewScheduleReferralVendor6Service.GetViewScheduleReferralVendor6(ViewScheduleReferralVendor6_name);

            if (ViewScheduleReferralVendor6s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor6 found for uci: {ViewScheduleReferralVendor6_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor6s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor6>> AddViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {
            var dbViewScheduleReferralVendor6 = await _ViewScheduleReferralVendor6Service.AddViewScheduleReferralVendor6(ViewScheduleReferralVendor6);

            if (dbViewScheduleReferralVendor6 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor6.TbViewScheduleReferralVendor6Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor6", new { uci = ViewScheduleReferralVendor6.TbViewScheduleReferralVendor6Name }, ViewScheduleReferralVendor6);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {           
            ViewScheduleReferralVendor6 dbViewScheduleReferralVendor6 = await _ViewScheduleReferralVendor6Service.UpdateViewScheduleReferralVendor6(ViewScheduleReferralVendor6);

            if (dbViewScheduleReferralVendor6 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor6.TbViewScheduleReferralVendor6Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor6Service.DeleteViewScheduleReferralVendor6(ViewScheduleReferralVendor6);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor6);
        }
    }
}
