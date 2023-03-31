using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrxSumByMonthController : ControllerBase
    {
        private readonly ITrxSumByMonthService _TrxSumByMonthService;

        public TrxSumByMonthController(ITrxSumByMonthService TrxSumByMonthService)
        {
            _TrxSumByMonthService = TrxSumByMonthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxSumByMonthList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TrxSumByMonths = await _TrxSumByMonthService.GetTrxSumByMonthListByValue(offset, limit, val);

            if (TrxSumByMonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TrxSumByMonths in database");
            }

            return StatusCode(StatusCodes.Status200OK, TrxSumByMonths);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxSumByMonthList(string TrxSumByMonth_name)
        {
            var TrxSumByMonths = await _TrxSumByMonthService.GetTrxSumByMonthList(TrxSumByMonth_name);

            if (TrxSumByMonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxSumByMonth found for uci: {TrxSumByMonth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxSumByMonths);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxSumByMonth(string TrxSumByMonth_name)
        {
            var TrxSumByMonths = await _TrxSumByMonthService.GetTrxSumByMonth(TrxSumByMonth_name);

            if (TrxSumByMonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxSumByMonth found for uci: {TrxSumByMonth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxSumByMonths);
        }

        [HttpPost]
        public async Task<ActionResult<TrxSumByMonth>> AddTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {
            var dbTrxSumByMonth = await _TrxSumByMonthService.AddTrxSumByMonth(TrxSumByMonth);

            if (dbTrxSumByMonth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxSumByMonth.TbTrxSumByMonthName} could not be added."
                );
            }

            return CreatedAtAction("GetTrxSumByMonth", new { uci = TrxSumByMonth.TbTrxSumByMonthName }, TrxSumByMonth);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {           
            TrxSumByMonth dbTrxSumByMonth = await _TrxSumByMonthService.UpdateTrxSumByMonth(TrxSumByMonth);

            if (dbTrxSumByMonth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxSumByMonth.TbTrxSumByMonthName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {            
            (bool status, string message) = await _TrxSumByMonthService.DeleteTrxSumByMonth(TrxSumByMonth);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TrxSumByMonth);
        }
    }
}
