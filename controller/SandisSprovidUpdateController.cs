using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSprovidUpdateController : ControllerBase
    {
        private readonly ISandisSprovidUpdateService _SandisSprovidUpdateService;

        public SandisSprovidUpdateController(ISandisSprovidUpdateService SandisSprovidUpdateService)
        {
            _SandisSprovidUpdateService = SandisSprovidUpdateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovidUpdateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSprovidUpdates = await _SandisSprovidUpdateService.GetSandisSprovidUpdateListByValue(offset, limit, val);

            if (SandisSprovidUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSprovidUpdates in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovidUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovidUpdateList(string SandisSprovidUpdate_name)
        {
            var SandisSprovidUpdates = await _SandisSprovidUpdateService.GetSandisSprovidUpdateList(SandisSprovidUpdate_name);

            if (SandisSprovidUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprovidUpdate found for uci: {SandisSprovidUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovidUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovidUpdate(string SandisSprovidUpdate_name)
        {
            var SandisSprovidUpdates = await _SandisSprovidUpdateService.GetSandisSprovidUpdate(SandisSprovidUpdate_name);

            if (SandisSprovidUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprovidUpdate found for uci: {SandisSprovidUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovidUpdates);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSprovidUpdate>> AddSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {
            var dbSandisSprovidUpdate = await _SandisSprovidUpdateService.AddSandisSprovidUpdate(SandisSprovidUpdate);

            if (dbSandisSprovidUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprovidUpdate.TbSandisSprovidUpdateName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSprovidUpdate", new { uci = SandisSprovidUpdate.TbSandisSprovidUpdateName }, SandisSprovidUpdate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {           
            SandisSprovidUpdate dbSandisSprovidUpdate = await _SandisSprovidUpdateService.UpdateSandisSprovidUpdate(SandisSprovidUpdate);

            if (dbSandisSprovidUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprovidUpdate.TbSandisSprovidUpdateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {            
            (bool status, string message) = await _SandisSprovidUpdateService.DeleteSandisSprovidUpdate(SandisSprovidUpdate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovidUpdate);
        }
    }
}
