using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewLangController : ControllerBase
    {
        private readonly IViewLangService _ViewLangService;

        public ViewLangController(IViewLangService ViewLangService)
        {
            _ViewLangService = ViewLangService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLangList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewLangs = await _ViewLangService.GetViewLangListByValue(offset, limit, val);

            if (ViewLangs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewLangs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLangs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLangList(string ViewLang_name)
        {
            var ViewLangs = await _ViewLangService.GetViewLangList(ViewLang_name);

            if (ViewLangs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLang found for uci: {ViewLang_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLangs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLang(string ViewLang_name)
        {
            var ViewLangs = await _ViewLangService.GetViewLang(ViewLang_name);

            if (ViewLangs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLang found for uci: {ViewLang_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLangs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewLang>> AddViewLang(ViewLang ViewLang)
        {
            var dbViewLang = await _ViewLangService.AddViewLang(ViewLang);

            if (dbViewLang == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLang.TbViewLangName} could not be added."
                );
            }

            return CreatedAtAction("GetViewLang", new { uci = ViewLang.TbViewLangName }, ViewLang);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewLang(ViewLang ViewLang)
        {           
            ViewLang dbViewLang = await _ViewLangService.UpdateViewLang(ViewLang);

            if (dbViewLang == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLang.TbViewLangName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewLang(ViewLang ViewLang)
        {            
            (bool status, string message) = await _ViewLangService.DeleteViewLang(ViewLang);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewLang);
        }
    }
}
