using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclirelDobController : ControllerBase
    {
        private readonly ISclirelDobService _SclirelDobService;

        public SclirelDobController(ISclirelDobService SclirelDobService)
        {
            _SclirelDobService = SclirelDobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirelDobList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SclirelDobs = await _SclirelDobService.GetSclirelDobListByValue(offset, limit, val);

            if (SclirelDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SclirelDobs in database");
            }

            return StatusCode(StatusCodes.Status200OK, SclirelDobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirelDobList(string SclirelDob_name)
        {
            var SclirelDobs = await _SclirelDobService.GetSclirelDobList(SclirelDob_name);

            if (SclirelDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclirelDob found for uci: {SclirelDob_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclirelDobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirelDob(string SclirelDob_name)
        {
            var SclirelDobs = await _SclirelDobService.GetSclirelDob(SclirelDob_name);

            if (SclirelDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclirelDob found for uci: {SclirelDob_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclirelDobs);
        }

        [HttpPost]
        public async Task<ActionResult<SclirelDob>> AddSclirelDob(SclirelDob SclirelDob)
        {
            var dbSclirelDob = await _SclirelDobService.AddSclirelDob(SclirelDob);

            if (dbSclirelDob == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclirelDob.TbSclirelDobName} could not be added."
                );
            }

            return CreatedAtAction("GetSclirelDob", new { uci = SclirelDob.TbSclirelDobName }, SclirelDob);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclirelDob(SclirelDob SclirelDob)
        {           
            SclirelDob dbSclirelDob = await _SclirelDobService.UpdateSclirelDob(SclirelDob);

            if (dbSclirelDob == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclirelDob.TbSclirelDobName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclirelDob(SclirelDob SclirelDob)
        {            
            (bool status, string message) = await _SclirelDobService.DeleteSclirelDob(SclirelDob);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SclirelDob);
        }
    }
}
