using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderSclicdrController : ControllerBase
    {
        private readonly ICderSclicdrService _CderSclicdrService;

        public CderSclicdrController(ICderSclicdrService CderSclicdrService)
        {
            _CderSclicdrService = CderSclicdrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderSclicdrs = await _CderSclicdrService.GetCderSclicdrListByValue(offset, limit, val);

            if (CderSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderSclicdrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdrList(string CderSclicdr_name)
        {
            var CderSclicdrs = await _CderSclicdrService.GetCderSclicdrList(CderSclicdr_name);

            if (CderSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSclicdr found for uci: {CderSclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderSclicdr(string CderSclicdr_name)
        {
            var CderSclicdrs = await _CderSclicdrService.GetCderSclicdr(CderSclicdr_name);

            if (CderSclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderSclicdr found for uci: {CderSclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdrs);
        }

        [HttpPost]
        public async Task<ActionResult<CderSclicdr>> AddCderSclicdr(CderSclicdr CderSclicdr)
        {
            var dbCderSclicdr = await _CderSclicdrService.AddCderSclicdr(CderSclicdr);

            if (dbCderSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSclicdr.TbCderSclicdrName} could not be added."
                );
            }

            return CreatedAtAction("GetCderSclicdr", new { uci = CderSclicdr.TbCderSclicdrName }, CderSclicdr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderSclicdr(CderSclicdr CderSclicdr)
        {           
            CderSclicdr dbCderSclicdr = await _CderSclicdrService.UpdateCderSclicdr(CderSclicdr);

            if (dbCderSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderSclicdr.TbCderSclicdrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderSclicdr(CderSclicdr CderSclicdr)
        {            
            (bool status, string message) = await _CderSclicdrService.DeleteCderSclicdr(CderSclicdr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderSclicdr);
        }
    }
}
