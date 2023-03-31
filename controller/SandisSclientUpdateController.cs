using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSclientUpdateController : ControllerBase
    {
        private readonly ISandisSclientUpdateService _SandisSclientUpdateService;

        public SandisSclientUpdateController(ISandisSclientUpdateService SandisSclientUpdateService)
        {
            _SandisSclientUpdateService = SandisSclientUpdateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclientUpdateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSclientUpdates = await _SandisSclientUpdateService.GetSandisSclientUpdateListByValue(offset, limit, val);

            if (SandisSclientUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSclientUpdates in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclientUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclientUpdateList(string SandisSclientUpdate_name)
        {
            var SandisSclientUpdates = await _SandisSclientUpdateService.GetSandisSclientUpdateList(SandisSclientUpdate_name);

            if (SandisSclientUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSclientUpdate found for uci: {SandisSclientUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclientUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclientUpdate(string SandisSclientUpdate_name)
        {
            var SandisSclientUpdates = await _SandisSclientUpdateService.GetSandisSclientUpdate(SandisSclientUpdate_name);

            if (SandisSclientUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSclientUpdate found for uci: {SandisSclientUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclientUpdates);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSclientUpdate>> AddSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {
            var dbSandisSclientUpdate = await _SandisSclientUpdateService.AddSandisSclientUpdate(SandisSclientUpdate);

            if (dbSandisSclientUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSclientUpdate.TbSandisSclientUpdateName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSclientUpdate", new { uci = SandisSclientUpdate.TbSandisSclientUpdateName }, SandisSclientUpdate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {           
            SandisSclientUpdate dbSandisSclientUpdate = await _SandisSclientUpdateService.UpdateSandisSclientUpdate(SandisSclientUpdate);

            if (dbSandisSclientUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSclientUpdate.TbSandisSclientUpdateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {            
            (bool status, string message) = await _SandisSclientUpdateService.DeleteSandisSclientUpdate(SandisSclientUpdate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclientUpdate);
        }
    }
}
