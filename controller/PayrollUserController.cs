using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollUserController : ControllerBase
    {
        private readonly IPayrollUserService _PayrollUserService;

        public PayrollUserController(IPayrollUserService PayrollUserService)
        {
            _PayrollUserService = PayrollUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollUserList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PayrollUsers = await _PayrollUserService.GetPayrollUserListByValue(offset, limit, val);

            if (PayrollUsers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PayrollUsers in database");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollUsers);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollUserList(string PayrollUser_name)
        {
            var PayrollUsers = await _PayrollUserService.GetPayrollUserList(PayrollUser_name);

            if (PayrollUsers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollUser found for uci: {PayrollUser_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollUsers);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollUser(string PayrollUser_name)
        {
            var PayrollUsers = await _PayrollUserService.GetPayrollUser(PayrollUser_name);

            if (PayrollUsers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PayrollUser found for uci: {PayrollUser_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PayrollUsers);
        }

        [HttpPost]
        public async Task<ActionResult<PayrollUser>> AddPayrollUser(PayrollUser PayrollUser)
        {
            var dbPayrollUser = await _PayrollUserService.AddPayrollUser(PayrollUser);

            if (dbPayrollUser == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollUser.TbPayrollUserName} could not be added."
                );
            }

            return CreatedAtAction("GetPayrollUser", new { uci = PayrollUser.TbPayrollUserName }, PayrollUser);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayrollUser(PayrollUser PayrollUser)
        {           
            PayrollUser dbPayrollUser = await _PayrollUserService.UpdatePayrollUser(PayrollUser);

            if (dbPayrollUser == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PayrollUser.TbPayrollUserName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayrollUser(PayrollUser PayrollUser)
        {            
            (bool status, string message) = await _PayrollUserService.DeletePayrollUser(PayrollUser);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PayrollUser);
        }
    }
}
