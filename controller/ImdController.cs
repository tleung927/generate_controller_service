using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImdController : ControllerBase
    {
        private readonly IImdService _ImdService;

        public ImdController(IImdService ImdService)
        {
            _ImdService = ImdService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImdList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Imds = await _ImdService.GetImdListByValue(offset, limit, val);

            if (Imds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Imds in database");
            }

            return StatusCode(StatusCodes.Status200OK, Imds);
        }

        [HttpGet]
        public async Task<IActionResult> GetImdList(string Imd_name)
        {
            var Imds = await _ImdService.GetImdList(Imd_name);

            if (Imds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Imd found for uci: {Imd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Imds);
        }

        [HttpGet]
        public async Task<IActionResult> GetImd(string Imd_name)
        {
            var Imds = await _ImdService.GetImd(Imd_name);

            if (Imds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Imd found for uci: {Imd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Imds);
        }

        [HttpPost]
        public async Task<ActionResult<Imd>> AddImd(Imd Imd)
        {
            var dbImd = await _ImdService.AddImd(Imd);

            if (dbImd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Imd.TbImdName} could not be added."
                );
            }

            return CreatedAtAction("GetImd", new { uci = Imd.TbImdName }, Imd);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImd(Imd Imd)
        {           
            Imd dbImd = await _ImdService.UpdateImd(Imd);

            if (dbImd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Imd.TbImdName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImd(Imd Imd)
        {            
            (bool status, string message) = await _ImdService.DeleteImd(Imd);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Imd);
        }
    }
}
