using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PbcatedtController : ControllerBase
    {
        private readonly IPbcatedtService _PbcatedtService;

        public PbcatedtController(IPbcatedtService PbcatedtService)
        {
            _PbcatedtService = PbcatedtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatedtList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pbcatedts = await _PbcatedtService.GetPbcatedtListByValue(offset, limit, val);

            if (Pbcatedts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pbcatedts in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatedts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatedtList(string Pbcatedt_name)
        {
            var Pbcatedts = await _PbcatedtService.GetPbcatedtList(Pbcatedt_name);

            if (Pbcatedts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatedt found for uci: {Pbcatedt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatedts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatedt(string Pbcatedt_name)
        {
            var Pbcatedts = await _PbcatedtService.GetPbcatedt(Pbcatedt_name);

            if (Pbcatedts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatedt found for uci: {Pbcatedt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatedts);
        }

        [HttpPost]
        public async Task<ActionResult<Pbcatedt>> AddPbcatedt(Pbcatedt Pbcatedt)
        {
            var dbPbcatedt = await _PbcatedtService.AddPbcatedt(Pbcatedt);

            if (dbPbcatedt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatedt.TbPbcatedtName} could not be added."
                );
            }

            return CreatedAtAction("GetPbcatedt", new { uci = Pbcatedt.TbPbcatedtName }, Pbcatedt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePbcatedt(Pbcatedt Pbcatedt)
        {           
            Pbcatedt dbPbcatedt = await _PbcatedtService.UpdatePbcatedt(Pbcatedt);

            if (dbPbcatedt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatedt.TbPbcatedtName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePbcatedt(Pbcatedt Pbcatedt)
        {            
            (bool status, string message) = await _PbcatedtService.DeletePbcatedt(Pbcatedt);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatedt);
        }
    }
}
