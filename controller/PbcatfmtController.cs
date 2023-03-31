using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PbcatfmtController : ControllerBase
    {
        private readonly IPbcatfmtService _PbcatfmtService;

        public PbcatfmtController(IPbcatfmtService PbcatfmtService)
        {
            _PbcatfmtService = PbcatfmtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatfmtList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pbcatfmts = await _PbcatfmtService.GetPbcatfmtListByValue(offset, limit, val);

            if (Pbcatfmts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pbcatfmts in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatfmts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatfmtList(string Pbcatfmt_name)
        {
            var Pbcatfmts = await _PbcatfmtService.GetPbcatfmtList(Pbcatfmt_name);

            if (Pbcatfmts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatfmt found for uci: {Pbcatfmt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatfmts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatfmt(string Pbcatfmt_name)
        {
            var Pbcatfmts = await _PbcatfmtService.GetPbcatfmt(Pbcatfmt_name);

            if (Pbcatfmts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatfmt found for uci: {Pbcatfmt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatfmts);
        }

        [HttpPost]
        public async Task<ActionResult<Pbcatfmt>> AddPbcatfmt(Pbcatfmt Pbcatfmt)
        {
            var dbPbcatfmt = await _PbcatfmtService.AddPbcatfmt(Pbcatfmt);

            if (dbPbcatfmt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatfmt.TbPbcatfmtName} could not be added."
                );
            }

            return CreatedAtAction("GetPbcatfmt", new { uci = Pbcatfmt.TbPbcatfmtName }, Pbcatfmt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePbcatfmt(Pbcatfmt Pbcatfmt)
        {           
            Pbcatfmt dbPbcatfmt = await _PbcatfmtService.UpdatePbcatfmt(Pbcatfmt);

            if (dbPbcatfmt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatfmt.TbPbcatfmtName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePbcatfmt(Pbcatfmt Pbcatfmt)
        {            
            (bool status, string message) = await _PbcatfmtService.DeletePbcatfmt(Pbcatfmt);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatfmt);
        }
    }
}
