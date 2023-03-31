using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupSclicdrController : ControllerBase
    {
        private readonly ICderBackupSclicdrService _CderBackupSclicdrService;

        public CderBackupSclicdrController(ICderBackupSclicdrService CderBackupSclicdrService)
        {
            _CderBackupSclicdrService = CderBackupSclicdrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackupSclicdrs = await _CderBackupSclicdrService.GetCderBackupSclicdrListByValue(offset, limit, val);

            if (CderBackupSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackupSclicdrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdrList(string CderBackupSclicdr_name)
        {
            var CderBackupSclicdrs = await _CderBackupSclicdrService.GetCderBackupSclicdrList(CderBackupSclicdr_name);

            if (CderBackupSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdr found for uci: {CderBackupSclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdr(string CderBackupSclicdr_name)
        {
            var CderBackupSclicdrs = await _CderBackupSclicdrService.GetCderBackupSclicdr(CderBackupSclicdr_name);

            if (CderBackupSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdr found for uci: {CderBackupSclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdrs);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackupSclicdr>> AddCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {
            var dbCderBackupSclicdr = await _CderBackupSclicdrService.AddCderBackupSclicdr(CderBackupSclicdr);

            if (dbCderBackupSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdr.TbCderBackupSclicdrName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackupSclicdr", new { uci = CderBackupSclicdr.TbCderBackupSclicdrName }, CderBackupSclicdr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {           
            CderBackupSclicdr dbCderBackupSclicdr = await _CderBackupSclicdrService.UpdateCderBackupSclicdr(CderBackupSclicdr);

            if (dbCderBackupSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdr.TbCderBackupSclicdrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {            
            (bool status, string message) = await _CderBackupSclicdrService.DeleteCderBackupSclicdr(CderBackupSclicdr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdr);
        }
    }
}
