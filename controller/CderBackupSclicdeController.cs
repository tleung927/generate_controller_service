using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderBackupSclicdeController : ControllerBase
    {
        private readonly ICderBackupSclicdeService _CderBackupSclicdeService;

        public CderBackupSclicdeController(ICderBackupSclicdeService CderBackupSclicdeService)
        {
            _CderBackupSclicdeService = CderBackupSclicdeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderBackupSclicdes = await _CderBackupSclicdeService.GetCderBackupSclicdeListByValue(offset, limit, val);

            if (CderBackupSclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderBackupSclicdes in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicdeList(string CderBackupSclicde_name)
        {
            var CderBackupSclicdes = await _CderBackupSclicdeService.GetCderBackupSclicdeList(CderBackupSclicde_name);

            if (CderBackupSclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicde found for uci: {CderBackupSclicde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderBackupSclicde(string CderBackupSclicde_name)
        {
            var CderBackupSclicdes = await _CderBackupSclicdeService.GetCderBackupSclicde(CderBackupSclicde_name);

            if (CderBackupSclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderBackupSclicde found for uci: {CderBackupSclicde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicdes);
        }

        [HttpPost]
        public async Task<ActionResult<CderBackupSclicde>> AddCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {
            var dbCderBackupSclicde = await _CderBackupSclicdeService.AddCderBackupSclicde(CderBackupSclicde);

            if (dbCderBackupSclicde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicde.TbCderBackupSclicdeName} could not be added."
                );
            }

            return CreatedAtAction("GetCderBackupSclicde", new { uci = CderBackupSclicde.TbCderBackupSclicdeName }, CderBackupSclicde);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {           
            CderBackupSclicde dbCderBackupSclicde = await _CderBackupSclicdeService.UpdateCderBackupSclicde(CderBackupSclicde);

            if (dbCderBackupSclicde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderBackupSclicde.TbCderBackupSclicdeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {            
            (bool status, string message) = await _CderBackupSclicdeService.DeleteCderBackupSclicde(CderBackupSclicde);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderBackupSclicde);
        }
    }
}
