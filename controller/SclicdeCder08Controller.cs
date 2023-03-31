using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclicdeCder08Controller : ControllerBase
    {
        private readonly ISclicdeCder08Service _SclicdeCder08Service;

        public SclicdeCder08Controller(ISclicdeCder08Service SclicdeCder08Service)
        {
            _SclicdeCder08Service = SclicdeCder08Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeCder08List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SclicdeCder08s = await _SclicdeCder08Service.GetSclicdeCder08ListByValue(offset, limit, val);

            if (SclicdeCder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SclicdeCder08s in database");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeCder08s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeCder08List(string SclicdeCder08_name)
        {
            var SclicdeCder08s = await _SclicdeCder08Service.GetSclicdeCder08List(SclicdeCder08_name);

            if (SclicdeCder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclicdeCder08 found for uci: {SclicdeCder08_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeCder08s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeCder08(string SclicdeCder08_name)
        {
            var SclicdeCder08s = await _SclicdeCder08Service.GetSclicdeCder08(SclicdeCder08_name);

            if (SclicdeCder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SclicdeCder08 found for uci: {SclicdeCder08_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeCder08s);
        }

        [HttpPost]
        public async Task<ActionResult<SclicdeCder08>> AddSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {
            var dbSclicdeCder08 = await _SclicdeCder08Service.AddSclicdeCder08(SclicdeCder08);

            if (dbSclicdeCder08 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclicdeCder08.TbSclicdeCder08Name} could not be added."
                );
            }

            return CreatedAtAction("GetSclicdeCder08", new { uci = SclicdeCder08.TbSclicdeCder08Name }, SclicdeCder08);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {           
            SclicdeCder08 dbSclicdeCder08 = await _SclicdeCder08Service.UpdateSclicdeCder08(SclicdeCder08);

            if (dbSclicdeCder08 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SclicdeCder08.TbSclicdeCder08Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {            
            (bool status, string message) = await _SclicdeCder08Service.DeleteSclicdeCder08(SclicdeCder08);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SclicdeCder08);
        }
    }
}
