using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RcController : ControllerBase
    {
        private readonly IRcService _RcService;

        public RcController(IRcService RcService)
        {
            _RcService = RcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRcList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Rcs = await _RcService.GetRcListByValue(offset, limit, val);

            if (Rcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Rcs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Rcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetRcList(string Rc_name)
        {
            var Rcs = await _RcService.GetRcList(Rc_name);

            if (Rcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rc found for uci: {Rc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetRc(string Rc_name)
        {
            var Rcs = await _RcService.GetRc(Rc_name);

            if (Rcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rc found for uci: {Rc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rcs);
        }

        [HttpPost]
        public async Task<ActionResult<Rc>> AddRc(Rc Rc)
        {
            var dbRc = await _RcService.AddRc(Rc);

            if (dbRc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rc.TbRcName} could not be added."
                );
            }

            return CreatedAtAction("GetRc", new { uci = Rc.TbRcName }, Rc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRc(Rc Rc)
        {           
            Rc dbRc = await _RcService.UpdateRc(Rc);

            if (dbRc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rc.TbRcName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRc(Rc Rc)
        {            
            (bool status, string message) = await _RcService.DeleteRc(Rc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Rc);
        }
    }
}
