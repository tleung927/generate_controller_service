using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _HolidayService;

        public HolidayController(IHolidayService HolidayService)
        {
            _HolidayService = HolidayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidayList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Holidays = await _HolidayService.GetHolidayListByValue(offset, limit, val);

            if (Holidays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Holidays in database");
            }

            return StatusCode(StatusCodes.Status200OK, Holidays);
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidayList(string Holiday_name)
        {
            var Holidays = await _HolidayService.GetHolidayList(Holiday_name);

            if (Holidays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Holiday found for uci: {Holiday_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Holidays);
        }

        [HttpGet]
        public async Task<IActionResult> GetHoliday(string Holiday_name)
        {
            var Holidays = await _HolidayService.GetHoliday(Holiday_name);

            if (Holidays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Holiday found for uci: {Holiday_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Holidays);
        }

        [HttpPost]
        public async Task<ActionResult<Holiday>> AddHoliday(Holiday Holiday)
        {
            var dbHoliday = await _HolidayService.AddHoliday(Holiday);

            if (dbHoliday == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Holiday.TbHolidayName} could not be added."
                );
            }

            return CreatedAtAction("GetHoliday", new { uci = Holiday.TbHolidayName }, Holiday);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHoliday(Holiday Holiday)
        {           
            Holiday dbHoliday = await _HolidayService.UpdateHoliday(Holiday);

            if (dbHoliday == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Holiday.TbHolidayName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHoliday(Holiday Holiday)
        {            
            (bool status, string message) = await _HolidayService.DeleteHoliday(Holiday);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Holiday);
        }
    }
}
