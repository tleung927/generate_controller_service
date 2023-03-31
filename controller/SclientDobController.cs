using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclientDobController : ControllerBase
    {
        private readonly ISclientDobService _SclientDobService;

        public SclientDobController(ISclientDobService SclientDobService)
        {
            _SclientDobService = SclientDobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDobList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SclientDobs = await _SclientDobService.GetSclientDobListByValue(offset, limit, val);

            if (SclientDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SclientDobs in database");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDobList(string SclientDob_name)
        {
            var SclientDobs = await _SclientDobService.GetSclientDobList(SclientDob_name);

            if (SclientDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclientDob found for uci: {SclientDob_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDob(string SclientDob_name)
        {
            var SclientDobs = await _SclientDobService.GetSclientDob(SclientDob_name);

            if (SclientDobs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclientDob found for uci: {SclientDob_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDobs);
        }

        [HttpPost]
        public async Task<ActionResult<SclientDob>> AddSclientDob(SclientDob SclientDob)
        {
            var dbSclientDob = await _SclientDobService.AddSclientDob(SclientDob);

            if (dbSclientDob == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclientDob.TbSclientDobName} could not be added."
                );
            }

            return CreatedAtAction("GetSclientDob", new { uci = SclientDob.TbSclientDobName }, SclientDob);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclientDob(SclientDob SclientDob)
        {           
            SclientDob dbSclientDob = await _SclientDobService.UpdateSclientDob(SclientDob);

            if (dbSclientDob == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclientDob.TbSclientDobName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclientDob(SclientDob SclientDob)
        {            
            (bool status, string message) = await _SclientDobService.DeleteSclientDob(SclientDob);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SclientDob);
        }
    }
}
