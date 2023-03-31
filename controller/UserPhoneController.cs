using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPhoneController : ControllerBase
    {
        private readonly IUserPhoneService _UserPhoneService;

        public UserPhoneController(IUserPhoneService UserPhoneService)
        {
            _UserPhoneService = UserPhoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPhoneList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var UserPhones = await _UserPhoneService.GetUserPhoneListByValue(offset, limit, val);

            if (UserPhones == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No UserPhones in database");
            }

            return StatusCode(StatusCodes.Status200OK, UserPhones);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPhoneList(string UserPhone_name)
        {
            var UserPhones = await _UserPhoneService.GetUserPhoneList(UserPhone_name);

            if (UserPhones == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UserPhone found for uci: {UserPhone_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UserPhones);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPhone(string UserPhone_name)
        {
            var UserPhones = await _UserPhoneService.GetUserPhone(UserPhone_name);

            if (UserPhones == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UserPhone found for uci: {UserPhone_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UserPhones);
        }

        [HttpPost]
        public async Task<ActionResult<UserPhone>> AddUserPhone(UserPhone UserPhone)
        {
            var dbUserPhone = await _UserPhoneService.AddUserPhone(UserPhone);

            if (dbUserPhone == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UserPhone.TbUserPhoneName} could not be added."
                );
            }

            return CreatedAtAction("GetUserPhone", new { uci = UserPhone.TbUserPhoneName }, UserPhone);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserPhone(UserPhone UserPhone)
        {           
            UserPhone dbUserPhone = await _UserPhoneService.UpdateUserPhone(UserPhone);

            if (dbUserPhone == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UserPhone.TbUserPhoneName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserPhone(UserPhone UserPhone)
        {            
            (bool status, string message) = await _UserPhoneService.DeleteUserPhone(UserPhone);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, UserPhone);
        }
    }
}
