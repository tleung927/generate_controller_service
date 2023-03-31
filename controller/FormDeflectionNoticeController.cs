using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormDeflectionNoticeController : ControllerBase
    {
        private readonly IFormDeflectionNoticeService _FormDeflectionNoticeService;

        public FormDeflectionNoticeController(IFormDeflectionNoticeService FormDeflectionNoticeService)
        {
            _FormDeflectionNoticeService = FormDeflectionNoticeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormDeflectionNoticeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormDeflectionNotices = await _FormDeflectionNoticeService.GetFormDeflectionNoticeListByValue(offset, limit, val);

            if (FormDeflectionNotices == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormDeflectionNotices in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormDeflectionNotices);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormDeflectionNoticeList(string FormDeflectionNotice_name)
        {
            var FormDeflectionNotices = await _FormDeflectionNoticeService.GetFormDeflectionNoticeList(FormDeflectionNotice_name);

            if (FormDeflectionNotices == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormDeflectionNotice found for uci: {FormDeflectionNotice_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormDeflectionNotices);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormDeflectionNotice(string FormDeflectionNotice_name)
        {
            var FormDeflectionNotices = await _FormDeflectionNoticeService.GetFormDeflectionNotice(FormDeflectionNotice_name);

            if (FormDeflectionNotices == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormDeflectionNotice found for uci: {FormDeflectionNotice_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormDeflectionNotices);
        }

        [HttpPost]
        public async Task<ActionResult<FormDeflectionNotice>> AddFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {
            var dbFormDeflectionNotice = await _FormDeflectionNoticeService.AddFormDeflectionNotice(FormDeflectionNotice);

            if (dbFormDeflectionNotice == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormDeflectionNotice.TbFormDeflectionNoticeName} could not be added."
                );
            }

            return CreatedAtAction("GetFormDeflectionNotice", new { uci = FormDeflectionNotice.TbFormDeflectionNoticeName }, FormDeflectionNotice);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {           
            FormDeflectionNotice dbFormDeflectionNotice = await _FormDeflectionNoticeService.UpdateFormDeflectionNotice(FormDeflectionNotice);

            if (dbFormDeflectionNotice == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormDeflectionNotice.TbFormDeflectionNoticeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {            
            (bool status, string message) = await _FormDeflectionNoticeService.DeleteFormDeflectionNotice(FormDeflectionNotice);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormDeflectionNotice);
        }
    }
}
