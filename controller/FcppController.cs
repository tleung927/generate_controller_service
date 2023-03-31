using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FcppController : ControllerBase
    {
        private readonly IFcppService _FcppService;

        public FcppController(IFcppService FcppService)
        {
            _FcppService = FcppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFcppList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Fcpps = await _FcppService.GetFcppListByValue(offset, limit, val);

            if (Fcpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Fcpps in database");
            }

            return StatusCode(StatusCodes.Status200OK, Fcpps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFcppList(string Fcpp_name)
        {
            var Fcpps = await _FcppService.GetFcppList(Fcpp_name);

            if (Fcpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Fcpp found for uci: {Fcpp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Fcpps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFcpp(string Fcpp_name)
        {
            var Fcpps = await _FcppService.GetFcpp(Fcpp_name);

            if (Fcpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Fcpp found for uci: {Fcpp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Fcpps);
        }

        [HttpPost]
        public async Task<ActionResult<Fcpp>> AddFcpp(Fcpp Fcpp)
        {
            var dbFcpp = await _FcppService.AddFcpp(Fcpp);

            if (dbFcpp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Fcpp.TbFcppName} could not be added."
                );
            }

            return CreatedAtAction("GetFcpp", new { uci = Fcpp.TbFcppName }, Fcpp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFcpp(Fcpp Fcpp)
        {           
            Fcpp dbFcpp = await _FcppService.UpdateFcpp(Fcpp);

            if (dbFcpp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Fcpp.TbFcppName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFcpp(Fcpp Fcpp)
        {            
            (bool status, string message) = await _FcppService.DeleteFcpp(Fcpp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Fcpp);
        }
    }
}
