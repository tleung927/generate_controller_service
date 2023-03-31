using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupController : ControllerBase
    {
        private readonly ICderBackupService _CderBackupService;

        public CderBackupController(ICderBackupService CderBackupService)
        {
            _CderBackupService = CderBackupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackups = await _CderBackupService.GetCderBackupListByValue(offset, limit, val);

            if (CderBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackups in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupList(string CderBackup_name)
        {
            var CderBackups = await _CderBackupService.GetCderBackupList(CderBackup_name);

            if (CderBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackup found for uci: {CderBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackup(string CderBackup_name)
        {
            var CderBackups = await _CderBackupService.GetCderBackup(CderBackup_name);

            if (CderBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackup found for uci: {CderBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackups);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackup>> AddCderBackup(CderBackup CderBackup)
        {
            var dbCderBackup = await _CderBackupService.AddCderBackup(CderBackup);

            if (dbCderBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackup.TbCderBackupName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackup", new { uci = CderBackup.TbCderBackupName }, CderBackup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackup(CderBackup CderBackup)
        {           
            CderBackup dbCderBackup = await _CderBackupService.UpdateCderBackup(CderBackup);

            if (dbCderBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackup.TbCderBackupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackup(CderBackup CderBackup)
        {            
            (bool status, string message) = await _CderBackupService.DeleteCderBackup(CderBackup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackup);
        }
    }
}
