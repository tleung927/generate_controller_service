using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollDetailController : ControllerBase
    {
        private readonly IPayrollDetailService _PayrollDetailService;

        public PayrollDetailController(IPayrollDetailService PayrollDetailService)
        {
            _PayrollDetailService = PayrollDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollDetailList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PayrollDetails = await _PayrollDetailService.GetPayrollDetailListByValue(offset, limit, val);

            if (PayrollDetails == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PayrollDetails in database");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollDetailList(string PayrollDetail_name)
        {
            var PayrollDetails = await _PayrollDetailService.GetPayrollDetailList(PayrollDetail_name);

            if (PayrollDetails == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollDetail found for uci: {PayrollDetail_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollDetail(string PayrollDetail_name)
        {
            var PayrollDetails = await _PayrollDetailService.GetPayrollDetail(PayrollDetail_name);

            if (PayrollDetails == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollDetail found for uci: {PayrollDetail_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollDetails);
        }

        [HttpPost]
        public async Task<ActionResult<PayrollDetail>> AddPayrollDetail(PayrollDetail PayrollDetail)
        {
            var dbPayrollDetail = await _PayrollDetailService.AddPayrollDetail(PayrollDetail);

            if (dbPayrollDetail == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollDetail.TbPayrollDetailName} could not be added."
                );
            }

            return CreatedAtAction("GetPayrollDetail", new { uci = PayrollDetail.TbPayrollDetailName }, PayrollDetail);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayrollDetail(PayrollDetail PayrollDetail)
        {           
            PayrollDetail dbPayrollDetail = await _PayrollDetailService.UpdatePayrollDetail(PayrollDetail);

            if (dbPayrollDetail == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollDetail.TbPayrollDetailName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayrollDetail(PayrollDetail PayrollDetail)
        {            
            (bool status, string message) = await _PayrollDetailService.DeletePayrollDetail(PayrollDetail);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PayrollDetail);
        }
    }
}
