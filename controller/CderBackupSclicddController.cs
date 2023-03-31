using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupSclicddController : ControllerBase
    {
        private readonly ICderBackupSclicddService _CderBackupSclicddService;

        public CderBackupSclicddController(ICderBackupSclicddService CderBackupSclicddService)
        {
            _CderBackupSclicddService = CderBackupSclicddService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicddList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackupSclicdds = await _CderBackupSclicddService.GetCderBackupSclicddListByValue(offset, limit, val);

            if (CderBackupSclicdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackupSclicdds in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdds);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicddList(string CderBackupSclicdd_name)
        {
            var CderBackupSclicdds = await _CderBackupSclicddService.GetCderBackupSclicddList(CderBackupSclicdd_name);

            if (CderBackupSclicdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdd found for uci: {CderBackupSclicdd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdds);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdd(string CderBackupSclicdd_name)
        {
            var CderBackupSclicdds = await _CderBackupSclicddService.GetCderBackupSclicdd(CderBackupSclicdd_name);

            if (CderBackupSclicdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicdd found for uci: {CderBackupSclicdd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdds);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackupSclicdd>> AddCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {
            var dbCderBackupSclicdd = await _CderBackupSclicddService.AddCderBackupSclicdd(CderBackupSclicdd);

            if (dbCderBackupSclicdd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdd.TbCderBackupSclicddName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackupSclicdd", new { uci = CderBackupSclicdd.TbCderBackupSclicddName }, CderBackupSclicdd);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {           
            CderBackupSclicdd dbCderBackupSclicdd = await _CderBackupSclicddService.UpdateCderBackupSclicdd(CderBackupSclicdd);

            if (dbCderBackupSclicdd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicdd.TbCderBackupSclicddName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {            
            (bool status, string message) = await _CderBackupSclicddService.DeleteCderBackupSclicdd(CderBackupSclicdd);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdd);
        }
    }
}
