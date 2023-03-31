using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderLabelController : ControllerBase
    {
        private readonly ICderLabelService _CderLabelService;

        public CderLabelController(ICderLabelService CderLabelService)
        {
            _CderLabelService = CderLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderLabels = await _CderLabelService.GetCderLabelListByValue(offset, limit, val);

            if (CderLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderLabelList(string CderLabel_name)
        {
            var CderLabels = await _CderLabelService.GetCderLabelList(CderLabel_name);

            if (CderLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderLabel found for uci: {CderLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderLabel(string CderLabel_name)
        {
            var CderLabels = await _CderLabelService.GetCderLabel(CderLabel_name);

            if (CderLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderLabel found for uci: {CderLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderLabels);
        }

        [HttpPost]
        public async Task<ActionResult<CderLabel>> AddCderLabel(CderLabel CderLabel)
        {
            var dbCderLabel = await _CderLabelService.AddCderLabel(CderLabel);

            if (dbCderLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderLabel.TbCderLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetCderLabel", new { uci = CderLabel.TbCderLabelName }, CderLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderLabel(CderLabel CderLabel)
        {           
            CderLabel dbCderLabel = await _CderLabelService.UpdateCderLabel(CderLabel);

            if (dbCderLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderLabel.TbCderLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderLabel(CderLabel CderLabel)
        {            
            (bool status, string message) = await _CderLabelService.DeleteCderLabel(CderLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderLabel);
        }
    }
}
