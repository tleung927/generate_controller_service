using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MwcController : ControllerBase
    {
        private readonly IMwcService _MwcService;

        public MwcController(IMwcService MwcService)
        {
            _MwcService = MwcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMwcList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Mwcs = await _MwcService.GetMwcListByValue(offset, limit, val);

            if (Mwcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Mwcs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Mwcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwcList(string Mwc_name)
        {
            var Mwcs = await _MwcService.GetMwcList(Mwc_name);

            if (Mwcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc found for uci: {Mwc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc(string Mwc_name)
        {
            var Mwcs = await _MwcService.GetMwc(Mwc_name);

            if (Mwcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc found for uci: {Mwc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwcs);
        }

        [HttpPost]
        public async Task<ActionResult<Mwc>> AddMwc(Mwc Mwc)
        {
            var dbMwc = await _MwcService.AddMwc(Mwc);

            if (dbMwc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc.TbMwcName} could not be added."
                );
            }

            return CreatedAtAction("GetMwc", new { uci = Mwc.TbMwcName }, Mwc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMwc(Mwc Mwc)
        {           
            Mwc dbMwc = await _MwcService.UpdateMwc(Mwc);

            if (dbMwc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc.TbMwcName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMwc(Mwc Mwc)
        {            
            (bool status, string message) = await _MwcService.DeleteMwc(Mwc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Mwc);
        }
    }
}
