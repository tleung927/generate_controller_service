using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cder08CommentController : ControllerBase
    {
        private readonly ICder08CommentService _Cder08CommentService;

        public Cder08CommentController(ICder08CommentService Cder08CommentService)
        {
            _Cder08CommentService = Cder08CommentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08CommentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Cder08Comments = await _Cder08CommentService.GetCder08CommentListByValue(offset, limit, val);

            if (Cder08Comments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Cder08Comments in database");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Comments);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08CommentList(string Cder08Comment_name)
        {
            var Cder08Comments = await _Cder08CommentService.GetCder08CommentList(Cder08Comment_name);

            if (Cder08Comments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Comment found for uci: {Cder08Comment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Comments);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08Comment(string Cder08Comment_name)
        {
            var Cder08Comments = await _Cder08CommentService.GetCder08Comment(Cder08Comment_name);

            if (Cder08Comments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Comment found for uci: {Cder08Comment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Comments);
        }

        [HttpPost]
        public async Task<ActionResult<Cder08Comment>> AddCder08Comment(Cder08Comment Cder08Comment)
        {
            var dbCder08Comment = await _Cder08CommentService.AddCder08Comment(Cder08Comment);

            if (dbCder08Comment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Comment.TbCder08CommentName} could not be added."
                );
            }

            return CreatedAtAction("GetCder08Comment", new { uci = Cder08Comment.TbCder08CommentName }, Cder08Comment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCder08Comment(Cder08Comment Cder08Comment)
        {           
            Cder08Comment dbCder08Comment = await _Cder08CommentService.UpdateCder08Comment(Cder08Comment);

            if (dbCder08Comment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Comment.TbCder08CommentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCder08Comment(Cder08Comment Cder08Comment)
        {            
            (bool status, string message) = await _Cder08CommentService.DeleteCder08Comment(Cder08Comment);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Comment);
        }
    }
}
