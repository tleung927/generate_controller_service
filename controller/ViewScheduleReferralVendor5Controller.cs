using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor5Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor5Service _ViewScheduleReferralVendor5Service;

        public ViewScheduleReferralVendor5Controller(IViewScheduleReferralVendor5Service ViewScheduleReferralVendor5Service)
        {
            _ViewScheduleReferralVendor5Service = ViewScheduleReferralVendor5Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor5List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor5s = await _ViewScheduleReferralVendor5Service.GetViewScheduleReferralVendor5ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor5s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor5s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor5s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor5List(string ViewScheduleReferralVendor5_name)
        {
            var ViewScheduleReferralVendor5s = await _ViewScheduleReferralVendor5Service.GetViewScheduleReferralVendor5List(ViewScheduleReferralVendor5_name);

            if (ViewScheduleReferralVendor5s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor5 found for uci: {ViewScheduleReferralVendor5_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor5s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor5(string ViewScheduleReferralVendor5_name)
        {
            var ViewScheduleReferralVendor5s = await _ViewScheduleReferralVendor5Service.GetViewScheduleReferralVendor5(ViewScheduleReferralVendor5_name);

            if (ViewScheduleReferralVendor5s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor5 found for uci: {ViewScheduleReferralVendor5_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor5s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor5>> AddViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {
            var dbViewScheduleReferralVendor5 = await _ViewScheduleReferralVendor5Service.AddViewScheduleReferralVendor5(ViewScheduleReferralVendor5);

            if (dbViewScheduleReferralVendor5 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor5.TbViewScheduleReferralVendor5Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor5", new { uci = ViewScheduleReferralVendor5.TbViewScheduleReferralVendor5Name }, ViewScheduleReferralVendor5);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {           
            ViewScheduleReferralVendor5 dbViewScheduleReferralVendor5 = await _ViewScheduleReferralVendor5Service.UpdateViewScheduleReferralVendor5(ViewScheduleReferralVendor5);

            if (dbViewScheduleReferralVendor5 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor5.TbViewScheduleReferralVendor5Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor5Service.DeleteViewScheduleReferralVendor5(ViewScheduleReferralVendor5);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor5);
        }
    }
}
