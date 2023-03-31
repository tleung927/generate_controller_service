using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirAddendumController : ControllerBase
    {
        private readonly ISirAddendumService _SirAddendumService;

        public SirAddendumController(ISirAddendumService SirAddendumService)
        {
            _SirAddendumService = SirAddendumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SirAddendums = await _SirAddendumService.GetSirAddendumListByValue(offset, limit, val);

            if (SirAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SirAddendums in database");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendumList(string SirAddendum_name)
        {
            var SirAddendums = await _SirAddendumService.GetSirAddendumList(SirAddendum_name);

            if (SirAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirAddendum found for uci: {SirAddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendum(string SirAddendum_name)
        {
            var SirAddendums = await _SirAddendumService.GetSirAddendum(SirAddendum_name);

            if (SirAddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirAddendum found for uci: {SirAddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendums);
        }

        [HttpPost]
        public async Task<ActionResult<SirAddendum>> AddSirAddendum(SirAddendum SirAddendum)
        {
            var dbSirAddendum = await _SirAddendumService.AddSirAddendum(SirAddendum);

            if (dbSirAddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirAddendum.TbSirAddendumName} could not be added."
                );
            }

            return CreatedAtAction("GetSirAddendum", new { uci = SirAddendum.TbSirAddendumName }, SirAddendum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSirAddendum(SirAddendum SirAddendum)
        {           
            SirAddendum dbSirAddendum = await _SirAddendumService.UpdateSirAddendum(SirAddendum);

            if (dbSirAddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirAddendum.TbSirAddendumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSirAddendum(SirAddendum SirAddendum)
        {            
            (bool status, string message) = await _SirAddendumService.DeleteSirAddendum(SirAddendum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendum);
        }
    }
}
