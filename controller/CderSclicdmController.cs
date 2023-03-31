using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderSclicdmController : ControllerBase
    {
        private readonly ICderSclicdmService _CderSclicdmService;

        public CderSclicdmController(ICderSclicdmService CderSclicdmService)
        {
            _CderSclicdmService = CderSclicdmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdmList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderSclicdms = await _CderSclicdmService.GetCderSclicdmListByValue(offset, limit, val);

            if (CderSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderSclicdms in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdmList(string CderSclicdm_name)
        {
            var CderSclicdms = await _CderSclicdmService.GetCderSclicdmList(CderSclicdm_name);

            if (CderSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSclicdm found for uci: {CderSclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdms);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdm(string CderSclicdm_name)
        {
            var CderSclicdms = await _CderSclicdmService.GetCderSclicdm(CderSclicdm_name);

            if (CderSclicdms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSclicdm found for uci: {CderSclicdm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdms);
        }

        [HttpPost]
        public async Task<ActionResult<CderSclicdm>> AddCderSclicdm(CderSclicdm CderSclicdm)
        {
            var dbCderSclicdm = await _CderSclicdmService.AddCderSclicdm(CderSclicdm);

            if (dbCderSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSclicdm.TbCderSclicdmName} could not be added."
                );
            }

            return CreatedAtAction("GetCderSclicdm", new { uci = CderSclicdm.TbCderSclicdmName }, CderSclicdm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderSclicdm(CderSclicdm CderSclicdm)
        {           
            CderSclicdm dbCderSclicdm = await _CderSclicdmService.UpdateCderSclicdm(CderSclicdm);

            if (dbCderSclicdm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSclicdm.TbCderSclicdmName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderSclicdm(CderSclicdm CderSclicdm)
        {            
            (bool status, string message) = await _CderSclicdmService.DeleteCderSclicdm(CderSclicdm);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdm);
        }
    }
}
