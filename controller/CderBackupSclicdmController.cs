using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupSclicdmController : ControllerBase
    {
        private readonly ICderBackupSclicdmService _CderBackupSclicdmService;

        public CderBackupSclicdmController(ICderBackupSclicdmService CderBackupSclicdmService)
        {
            _CderBackupSclicdmService = CderBackupSclicdmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdmList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackupSclicdms = await _CderBackupSclicdmService.GetCderBackupSclicdmListByValue(offset, limit, val);

            if (CderBackupSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackupSclicdms in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdmList(string CderBackupSclicdm_name)
        {
            var CderBackupSclicdms = await _CderBackupSclicdmService.GetCderBackupSclicdmList(CderBackupSclicdm_name);

            if (CderBackupSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdm found for uci: {CderBackupSclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdm(string CderBackupSclicdm_name)
        {
            var CderBackupSclicdms = await _CderBackupSclicdmService.GetCderBackupSclicdm(CderBackupSclicdm_name);

            if (CderBackupSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdm found for uci: {CderBackupSclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdms);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackupSclicdm>> AddCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {
            var dbCderBackupSclicdm = await _CderBackupSclicdmService.AddCderBackupSclicdm(CderBackupSclicdm);

            if (dbCderBackupSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdm.TbCderBackupSclicdmName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackupSclicdm", new { uci = CderBackupSclicdm.TbCderBackupSclicdmName }, CderBackupSclicdm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {           
            CderBackupSclicdm dbCderBackupSclicdm = await _CderBackupSclicdmService.UpdateCderBackupSclicdm(CderBackupSclicdm);

            if (dbCderBackupSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdm.TbCderBackupSclicdmName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {            
            (bool status, string message) = await _CderBackupSclicdmService.DeleteCderBackupSclicdm(CderBackupSclicdm);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdm);
        }
    }
}
