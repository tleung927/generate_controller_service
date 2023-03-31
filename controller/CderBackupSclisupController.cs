using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupSclisupController : ControllerBase
    {
        private readonly ICderBackupSclisupService _CderBackupSclisupService;

        public CderBackupSclisupController(ICderBackupSclisupService CderBackupSclisupService)
        {
            _CderBackupSclisupService = CderBackupSclisupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclisupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackupSclisups = await _CderBackupSclisupService.GetCderBackupSclisupListByValue(offset, limit, val);

            if (CderBackupSclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackupSclisups in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclisups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclisupList(string CderBackupSclisup_name)
        {
            var CderBackupSclisups = await _CderBackupSclisupService.GetCderBackupSclisupList(CderBackupSclisup_name);

            if (CderBackupSclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclisup found for uci: {CderBackupSclisup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclisups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclisup(string CderBackupSclisup_name)
        {
            var CderBackupSclisups = await _CderBackupSclisupService.GetCderBackupSclisup(CderBackupSclisup_name);

            if (CderBackupSclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclisup found for uci: {CderBackupSclisup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclisups);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackupSclisup>> AddCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {
            var dbCderBackupSclisup = await _CderBackupSclisupService.AddCderBackupSclisup(CderBackupSclisup);

            if (dbCderBackupSclisup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclisup.TbCderBackupSclisupName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackupSclisup", new { uci = CderBackupSclisup.TbCderBackupSclisupName }, CderBackupSclisup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {           
            CderBackupSclisup dbCderBackupSclisup = await _CderBackupSclisupService.UpdateCderBackupSclisup(CderBackupSclisup);

            if (dbCderBackupSclisup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclisup.TbCderBackupSclisupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {            
            (bool status, string message) = await _CderBackupSclisupService.DeleteCderBackupSclisup(CderBackupSclisup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclisup);
        }
    }
}
