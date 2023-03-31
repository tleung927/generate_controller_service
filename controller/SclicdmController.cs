using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclicdmController : ControllerBase
    {
        private readonly ISclicdmService _SclicdmService;

        public SclicdmController(ISclicdmService SclicdmService)
        {
            _SclicdmService = SclicdmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdmList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclicdms = await _SclicdmService.GetSclicdmListByValue(offset, limit, val);

            if (Sclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclicdms in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdmList(string Sclicdm_name)
        {
            var Sclicdms = await _SclicdmService.GetSclicdmList(Sclicdm_name);

            if (Sclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicdm found for uci: {Sclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdm(string Sclicdm_name)
        {
            var Sclicdms = await _SclicdmService.GetSclicdm(Sclicdm_name);

            if (Sclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicdm found for uci: {Sclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdms);
        }

        [HttpPost]
        public async Task<ActionResult<Sclicdm>> AddSclicdm(Sclicdm Sclicdm)
        {
            var dbSclicdm = await _SclicdmService.AddSclicdm(Sclicdm);

            if (dbSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicdm.TbSclicdmName} could not be added."
                );
            }

            return CreatedAtAction("GetSclicdm", new { uci = Sclicdm.TbSclicdmName }, Sclicdm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclicdm(Sclicdm Sclicdm)
        {           
            Sclicdm dbSclicdm = await _SclicdmService.UpdateSclicdm(Sclicdm);

            if (dbSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicdm.TbSclicdmName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclicdm(Sclicdm Sclicdm)
        {            
            (bool status, string message) = await _SclicdmService.DeleteSclicdm(Sclicdm);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdm);
        }
    }
}
