using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSprotaxUpdateController : ControllerBase
    {
        private readonly ISandisSprotaxUpdateService _SandisSprotaxUpdateService;

        public SandisSprotaxUpdateController(ISandisSprotaxUpdateService SandisSprotaxUpdateService)
        {
            _SandisSprotaxUpdateService = SandisSprotaxUpdateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSprotaxUpdates = await _SandisSprotaxUpdateService.GetSandisSprotaxUpdateListByValue(offset, limit, val);

            if (SandisSprotaxUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSprotaxUpdates in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdateList(string SandisSprotaxUpdate_name)
        {
            var SandisSprotaxUpdates = await _SandisSprotaxUpdateService.GetSandisSprotaxUpdateList(SandisSprotaxUpdate_name);

            if (SandisSprotaxUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotaxUpdate found for uci: {SandisSprotaxUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdates);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdate(string SandisSprotaxUpdate_name)
        {
            var SandisSprotaxUpdates = await _SandisSprotaxUpdateService.GetSandisSprotaxUpdate(SandisSprotaxUpdate_name);

            if (SandisSprotaxUpdates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotaxUpdate found for uci: {SandisSprotaxUpdate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdates);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSprotaxUpdate>> AddSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {
            var dbSandisSprotaxUpdate = await _SandisSprotaxUpdateService.AddSandisSprotaxUpdate(SandisSprotaxUpdate);

            if (dbSandisSprotaxUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotaxUpdate.TbSandisSprotaxUpdateName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSprotaxUpdate", new { uci = SandisSprotaxUpdate.TbSandisSprotaxUpdateName }, SandisSprotaxUpdate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {           
            SandisSprotaxUpdate dbSandisSprotaxUpdate = await _SandisSprotaxUpdateService.UpdateSandisSprotaxUpdate(SandisSprotaxUpdate);

            if (dbSandisSprotaxUpdate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotaxUpdate.TbSandisSprotaxUpdateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {            
            (bool status, string message) = await _SandisSprotaxUpdateService.DeleteSandisSprotaxUpdate(SandisSprotaxUpdate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdate);
        }
    }
}
