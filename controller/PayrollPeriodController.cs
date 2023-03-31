using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollPeriodController : ControllerBase
    {
        private readonly IPayrollPeriodService _PayrollPeriodService;

        public PayrollPeriodController(IPayrollPeriodService PayrollPeriodService)
        {
            _PayrollPeriodService = PayrollPeriodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollPeriodList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PayrollPeriods = await _PayrollPeriodService.GetPayrollPeriodListByValue(offset, limit, val);

            if (PayrollPeriods == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PayrollPeriods in database");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollPeriods);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollPeriodList(string PayrollPeriod_name)
        {
            var PayrollPeriods = await _PayrollPeriodService.GetPayrollPeriodList(PayrollPeriod_name);

            if (PayrollPeriods == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollPeriod found for uci: {PayrollPeriod_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollPeriods);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollPeriod(string PayrollPeriod_name)
        {
            var PayrollPeriods = await _PayrollPeriodService.GetPayrollPeriod(PayrollPeriod_name);

            if (PayrollPeriods == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollPeriod found for uci: {PayrollPeriod_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollPeriods);
        }

        [HttpPost]
        public async Task<ActionResult<PayrollPeriod>> AddPayrollPeriod(PayrollPeriod PayrollPeriod)
        {
            var dbPayrollPeriod = await _PayrollPeriodService.AddPayrollPeriod(PayrollPeriod);

            if (dbPayrollPeriod == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollPeriod.TbPayrollPeriodName} could not be added."
                );
            }

            return CreatedAtAction("GetPayrollPeriod", new { uci = PayrollPeriod.TbPayrollPeriodName }, PayrollPeriod);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayrollPeriod(PayrollPeriod PayrollPeriod)
        {           
            PayrollPeriod dbPayrollPeriod = await _PayrollPeriodService.UpdatePayrollPeriod(PayrollPeriod);

            if (dbPayrollPeriod == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollPeriod.TbPayrollPeriodName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayrollPeriod(PayrollPeriod PayrollPeriod)
        {            
            (bool status, string message) = await _PayrollPeriodService.DeletePayrollPeriod(PayrollPeriod);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PayrollPeriod);
        }
    }
}
