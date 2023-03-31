using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TsrController : ControllerBase
    {
        private readonly ITsrService _TsrService;

        public TsrController(ITsrService TsrService)
        {
            _TsrService = TsrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTsrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Tsrs = await _TsrService.GetTsrListByValue(offset, limit, val);

            if (Tsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Tsrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Tsrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTsrList(string Tsr_name)
        {
            var Tsrs = await _TsrService.GetTsrList(Tsr_name);

            if (Tsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tsr found for uci: {Tsr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tsrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTsr(string Tsr_name)
        {
            var Tsrs = await _TsrService.GetTsr(Tsr_name);

            if (Tsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tsr found for uci: {Tsr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tsrs);
        }

        [HttpPost]
        public async Task<ActionResult<Tsr>> AddTsr(Tsr Tsr)
        {
            var dbTsr = await _TsrService.AddTsr(Tsr);

            if (dbTsr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tsr.TbTsrName} could not be added."
                );
            }

            return CreatedAtAction("GetTsr", new { uci = Tsr.TbTsrName }, Tsr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTsr(Tsr Tsr)
        {           
            Tsr dbTsr = await _TsrService.UpdateTsr(Tsr);

            if (dbTsr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tsr.TbTsrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTsr(Tsr Tsr)
        {            
            (bool status, string message) = await _TsrService.DeleteTsr(Tsr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Tsr);
        }
    }
}
