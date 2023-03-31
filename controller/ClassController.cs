using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _ClassService;

        public ClassController(IClassService ClassService)
        {
            _ClassService = ClassService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Classs = await _ClassService.GetClassListByValue(offset, limit, val);

            if (Classs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Classs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Classs);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassList(string Class_name)
        {
            var Classs = await _ClassService.GetClassList(Class_name);

            if (Classs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Class found for uci: {Class_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Classs);
        }

        [HttpGet]
        public async Task<IActionResult> GetClass(string Class_name)
        {
            var Classs = await _ClassService.GetClass(Class_name);

            if (Classs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Class found for uci: {Class_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Classs);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> AddClass(Class Class)
        {
            var dbClass = await _ClassService.AddClass(Class);

            if (dbClass == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Class.TbClassName} could not be added."
                );
            }

            return CreatedAtAction("GetClass", new { uci = Class.TbClassName }, Class);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClass(Class Class)
        {           
            Class dbClass = await _ClassService.UpdateClass(Class);

            if (dbClass == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Class.TbClassName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClass(Class Class)
        {            
            (bool status, string message) = await _ClassService.DeleteClass(Class);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Class);
        }
    }
}
