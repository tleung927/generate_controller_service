using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MwcDevBackupController : ControllerBase
    {
        private readonly IMwcDevBackupService _MwcDevBackupService;

        public MwcDevBackupController(IMwcDevBackupService MwcDevBackupService)
        {
            _MwcDevBackupService = MwcDevBackupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMwcDevBackupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var MwcDevBackups = await _MwcDevBackupService.GetMwcDevBackupListByValue(offset, limit, val);

            if (MwcDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No MwcDevBackups in database");
            }

            return StatusCode(StatusCodes.Status200OK, MwcDevBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwcDevBackupList(string MwcDevBackup_name)
        {
            var MwcDevBackups = await _MwcDevBackupService.GetMwcDevBackupList(MwcDevBackup_name);

            if (MwcDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MwcDevBackup found for uci: {MwcDevBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MwcDevBackups);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwcDevBackup(string MwcDevBackup_name)
        {
            var MwcDevBackups = await _MwcDevBackupService.GetMwcDevBackup(MwcDevBackup_name);

            if (MwcDevBackups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MwcDevBackup found for uci: {MwcDevBackup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MwcDevBackups);
        }

        [HttpPost]
        public async Task<ActionResult<MwcDevBackup>> AddMwcDevBackup(MwcDevBackup MwcDevBackup)
        {
            var dbMwcDevBackup = await _MwcDevBackupService.AddMwcDevBackup(MwcDevBackup);

            if (dbMwcDevBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MwcDevBackup.TbMwcDevBackupName} could not be added."
                );
            }

            return CreatedAtAction("GetMwcDevBackup", new { uci = MwcDevBackup.TbMwcDevBackupName }, MwcDevBackup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMwcDevBackup(MwcDevBackup MwcDevBackup)
        {           
            MwcDevBackup dbMwcDevBackup = await _MwcDevBackupService.UpdateMwcDevBackup(MwcDevBackup);

            if (dbMwcDevBackup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MwcDevBackup.TbMwcDevBackupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMwcDevBackup(MwcDevBackup MwcDevBackup)
        {            
            (bool status, string message) = await _MwcDevBackupService.DeleteMwcDevBackup(MwcDevBackup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, MwcDevBackup);
        }
    }
}
