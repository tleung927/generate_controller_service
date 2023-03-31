using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollScheduleController : ControllerBase
    {
        private readonly IPayrollScheduleService _PayrollScheduleService;

        public PayrollScheduleController(IPayrollScheduleService PayrollScheduleService)
        {
            _PayrollScheduleService = PayrollScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollScheduleList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PayrollSchedules = await _PayrollScheduleService.GetPayrollScheduleListByValue(offset, limit, val);

            if (PayrollSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PayrollSchedules in database");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollSchedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollScheduleList(string PayrollSchedule_name)
        {
            var PayrollSchedules = await _PayrollScheduleService.GetPayrollScheduleList(PayrollSchedule_name);

            if (PayrollSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollSchedule found for uci: {PayrollSchedule_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollSchedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollSchedule(string PayrollSchedule_name)
        {
            var PayrollSchedules = await _PayrollScheduleService.GetPayrollSchedule(PayrollSchedule_name);

            if (PayrollSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollSchedule found for uci: {PayrollSchedule_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollSchedules);
        }

        [HttpPost]
        public async Task<ActionResult<PayrollSchedule>> AddPayrollSchedule(PayrollSchedule PayrollSchedule)
        {
            var dbPayrollSchedule = await _PayrollScheduleService.AddPayrollSchedule(PayrollSchedule);

            if (dbPayrollSchedule == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollSchedule.TbPayrollScheduleName} could not be added."
                );
            }

            return CreatedAtAction("GetPayrollSchedule", new { uci = PayrollSchedule.TbPayrollScheduleName }, PayrollSchedule);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayrollSchedule(PayrollSchedule PayrollSchedule)
        {           
            PayrollSchedule dbPayrollSchedule = await _PayrollScheduleService.UpdatePayrollSchedule(PayrollSchedule);

            if (dbPayrollSchedule == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollSchedule.TbPayrollScheduleName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayrollSchedule(PayrollSchedule PayrollSchedule)
        {            
            (bool status, string message) = await _PayrollScheduleService.DeletePayrollSchedule(PayrollSchedule);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PayrollSchedule);
        }
    }
}
