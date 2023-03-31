using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleReferralVendor3Controller : ControllerBase
    {
        private readonly IViewScheduleReferralVendor3Service _ViewScheduleReferralVendor3Service;

        public ViewScheduleReferralVendor3Controller(IViewScheduleReferralVendor3Service ViewScheduleReferralVendor3Service)
        {
            _ViewScheduleReferralVendor3Service = ViewScheduleReferralVendor3Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor3List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleReferralVendor3s = await _ViewScheduleReferralVendor3Service.GetViewScheduleReferralVendor3ListByValue(offset, limit, val);

            if (ViewScheduleReferralVendor3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleReferralVendor3s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor3List(string ViewScheduleReferralVendor3_name)
        {
            var ViewScheduleReferralVendor3s = await _ViewScheduleReferralVendor3Service.GetViewScheduleReferralVendor3List(ViewScheduleReferralVendor3_name);

            if (ViewScheduleReferralVendor3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor3 found for uci: {ViewScheduleReferralVendor3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleReferralVendor3(string ViewScheduleReferralVendor3_name)
        {
            var ViewScheduleReferralVendor3s = await _ViewScheduleReferralVendor3Service.GetViewScheduleReferralVendor3(ViewScheduleReferralVendor3_name);

            if (ViewScheduleReferralVendor3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleReferralVendor3 found for uci: {ViewScheduleReferralVendor3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor3s);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleReferralVendor3>> AddViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {
            var dbViewScheduleReferralVendor3 = await _ViewScheduleReferralVendor3Service.AddViewScheduleReferralVendor3(ViewScheduleReferralVendor3);

            if (dbViewScheduleReferralVendor3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor3.TbViewScheduleReferralVendor3Name} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleReferralVendor3", new { uci = ViewScheduleReferralVendor3.TbViewScheduleReferralVendor3Name }, ViewScheduleReferralVendor3);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {           
            ViewScheduleReferralVendor3 dbViewScheduleReferralVendor3 = await _ViewScheduleReferralVendor3Service.UpdateViewScheduleReferralVendor3(ViewScheduleReferralVendor3);

            if (dbViewScheduleReferralVendor3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleReferralVendor3.TbViewScheduleReferralVendor3Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {            
            (bool status, string message) = await _ViewScheduleReferralVendor3Service.DeleteViewScheduleReferralVendor3(ViewScheduleReferralVendor3);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleReferralVendor3);
        }
    }
}
