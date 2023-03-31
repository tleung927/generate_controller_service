using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewCderAutController : ControllerBase
    {
        private readonly IViewCderAutService _ViewCderAutService;

        public ViewCderAutController(IViewCderAutService ViewCderAutService)
        {
            _ViewCderAutService = ViewCderAutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderAutList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewCderAuts = await _ViewCderAutService.GetViewCderAutListByValue(offset, limit, val);

            if (ViewCderAuts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewCderAuts in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderAuts);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderAutList(string ViewCderAut_name)
        {
            var ViewCderAuts = await _ViewCderAutService.GetViewCderAutList(ViewCderAut_name);

            if (ViewCderAuts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderAut found for uci: {ViewCderAut_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderAuts);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderAut(string ViewCderAut_name)
        {
            var ViewCderAuts = await _ViewCderAutService.GetViewCderAut(ViewCderAut_name);

            if (ViewCderAuts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderAut found for uci: {ViewCderAut_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderAuts);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCderAut>> AddViewCderAut(ViewCderAut ViewCderAut)
        {
            var dbViewCderAut = await _ViewCderAutService.AddViewCderAut(ViewCderAut);

            if (dbViewCderAut == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderAut.TbViewCderAutName} could not be added."
                );
            }

            return CreatedAtAction("GetViewCderAut", new { uci = ViewCderAut.TbViewCderAutName }, ViewCderAut);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewCderAut(ViewCderAut ViewCderAut)
        {           
            ViewCderAut dbViewCderAut = await _ViewCderAutService.UpdateViewCderAut(ViewCderAut);

            if (dbViewCderAut == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderAut.TbViewCderAutName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewCderAut(ViewCderAut ViewCderAut)
        {            
            (bool status, string message) = await _ViewCderAutService.DeleteViewCderAut(ViewCderAut);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderAut);
        }
    }
}
