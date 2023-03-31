using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollTimecardController : ControllerBase
    {
        private readonly IPayrollTimecardService _PayrollTimecardService;

        public PayrollTimecardController(IPayrollTimecardService PayrollTimecardService)
        {
            _PayrollTimecardService = PayrollTimecardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollTimecardList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PayrollTimecards = await _PayrollTimecardService.GetPayrollTimecardListByValue(offset, limit, val);

            if (PayrollTimecards == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PayrollTimecards in database");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollTimecards);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollTimecardList(string PayrollTimecard_name)
        {
            var PayrollTimecards = await _PayrollTimecardService.GetPayrollTimecardList(PayrollTimecard_name);

            if (PayrollTimecards == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollTimecard found for uci: {PayrollTimecard_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollTimecards);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollTimecard(string PayrollTimecard_name)
        {
            var PayrollTimecards = await _PayrollTimecardService.GetPayrollTimecard(PayrollTimecard_name);

            if (PayrollTimecards == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollTimecard found for uci: {PayrollTimecard_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollTimecards);
        }

        [HttpPost]
        public async Task<ActionResult<PayrollTimecard>> AddPayrollTimecard(PayrollTimecard PayrollTimecard)
        {
            var dbPayrollTimecard = await _PayrollTimecardService.AddPayrollTimecard(PayrollTimecard);

            if (dbPayrollTimecard == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollTimecard.TbPayrollTimecardName} could not be added."
                );
            }

            return CreatedAtAction("GetPayrollTimecard", new { uci = PayrollTimecard.TbPayrollTimecardName }, PayrollTimecard);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayrollTimecard(PayrollTimecard PayrollTimecard)
        {           
            PayrollTimecard dbPayrollTimecard = await _PayrollTimecardService.UpdatePayrollTimecard(PayrollTimecard);

            if (dbPayrollTimecard == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollTimecard.TbPayrollTimecardName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayrollTimecard(PayrollTimecard PayrollTimecard)
        {            
            (bool status, string message) = await _PayrollTimecardService.DeletePayrollTimecard(PayrollTimecard);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PayrollTimecard);
        }
    }
}
