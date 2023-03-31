using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderSupController : ControllerBase
    {
        private readonly ICderSupService _CderSupService;

        public CderSupController(ICderSupService CderSupService)
        {
            _CderSupService = CderSupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderSups = await _CderSupService.GetCderSupListByValue(offset, limit, val);

            if (CderSups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderSups in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderSups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSupList(string CderSup_name)
        {
            var CderSups = await _CderSupService.GetCderSupList(CderSup_name);

            if (CderSups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSup found for uci: {CderSup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSups);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSup(string CderSup_name)
        {
            var CderSups = await _CderSupService.GetCderSup(CderSup_name);

            if (CderSups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSup found for uci: {CderSup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSups);
        }

        [HttpPost]
        public async Task<ActionResult<CderSup>> AddCderSup(CderSup CderSup)
        {
            var dbCderSup = await _CderSupService.AddCderSup(CderSup);

            if (dbCderSup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSup.TbCderSupName} could not be added."
                );
            }

            return CreatedAtAction("GetCderSup", new { uci = CderSup.TbCderSupName }, CderSup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderSup(CderSup CderSup)
        {           
            CderSup dbCderSup = await _CderSupService.UpdateCderSup(CderSup);

            if (dbCderSup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSup.TbCderSupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderSup(CderSup CderSup)
        {            
            (bool status, string message) = await _CderSupService.DeleteCderSup(CderSup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderSup);
        }
    }
}
