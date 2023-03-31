using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Users = await _UserService.GetUserListByValue(offset, limit, val);

            if (Users == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Users in database");
            }

            return StatusCode(StatusCodes.Status200OK, Users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList(string User_name)
        {
            var Users = await _UserService.GetUserList(User_name);

            if (Users == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No User found for uci: {User_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string User_name)
        {
            var Users = await _UserService.GetUser(User_name);

            if (Users == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No User found for uci: {User_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User User)
        {
            var dbUser = await _UserService.AddUser(User);

            if (dbUser == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{User.TbUserName} could not be added."
                );
            }

            return CreatedAtAction("GetUser", new { uci = User.TbUserName }, User);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User User)
        {           
            User dbUser = await _UserService.UpdateUser(User);

            if (dbUser == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{User.TbUserName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(User User)
        {            
            (bool status, string message) = await _UserService.DeleteUser(User);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, User);
        }
    }
}
