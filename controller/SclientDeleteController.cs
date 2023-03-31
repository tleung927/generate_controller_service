using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclientDeleteController : ControllerBase
    {
        private readonly ISclientDeleteService _SclientDeleteService;

        public SclientDeleteController(ISclientDeleteService SclientDeleteService)
        {
            _SclientDeleteService = SclientDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDeleteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SclientDeletes = await _SclientDeleteService.GetSclientDeleteListByValue(offset, limit, val);

            if (SclientDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SclientDeletes in database");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDeleteList(string SclientDelete_name)
        {
            var SclientDeletes = await _SclientDeleteService.GetSclientDeleteList(SclientDelete_name);

            if (SclientDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclientDelete found for uci: {SclientDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientDelete(string SclientDelete_name)
        {
            var SclientDeletes = await _SclientDeleteService.GetSclientDelete(SclientDelete_name);

            if (SclientDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclientDelete found for uci: {SclientDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclientDeletes);
        }

        [HttpPost]
        public async Task<ActionResult<SclientDelete>> AddSclientDelete(SclientDelete SclientDelete)
        {
            var dbSclientDelete = await _SclientDeleteService.AddSclientDelete(SclientDelete);

            if (dbSclientDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclientDelete.TbSclientDeleteName} could not be added."
                );
            }

            return CreatedAtAction("GetSclientDelete", new { uci = SclientDelete.TbSclientDeleteName }, SclientDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclientDelete(SclientDelete SclientDelete)
        {           
            SclientDelete dbSclientDelete = await _SclientDeleteService.UpdateSclientDelete(SclientDelete);

            if (dbSclientDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclientDelete.TbSclientDeleteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclientDelete(SclientDelete SclientDelete)
        {            
            (bool status, string message) = await _SclientDeleteService.DeleteSclientDelete(SclientDelete);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SclientDelete);
        }
    }
}
