using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugsLabelController : ControllerBase
    {
        private readonly IDrugsLabelService _DrugsLabelService;

        public DrugsLabelController(IDrugsLabelService DrugsLabelService)
        {
            _DrugsLabelService = DrugsLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugsLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DrugsLabels = await _DrugsLabelService.GetDrugsLabelListByValue(offset, limit, val);

            if (DrugsLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DrugsLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, DrugsLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugsLabelList(string DrugsLabel_name)
        {
            var DrugsLabels = await _DrugsLabelService.GetDrugsLabelList(DrugsLabel_name);

            if (DrugsLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DrugsLabel found for uci: {DrugsLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DrugsLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugsLabel(string DrugsLabel_name)
        {
            var DrugsLabels = await _DrugsLabelService.GetDrugsLabel(DrugsLabel_name);

            if (DrugsLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DrugsLabel found for uci: {DrugsLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DrugsLabels);
        }

        [HttpPost]
        public async Task<ActionResult<DrugsLabel>> AddDrugsLabel(DrugsLabel DrugsLabel)
        {
            var dbDrugsLabel = await _DrugsLabelService.AddDrugsLabel(DrugsLabel);

            if (dbDrugsLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DrugsLabel.TbDrugsLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetDrugsLabel", new { uci = DrugsLabel.TbDrugsLabelName }, DrugsLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDrugsLabel(DrugsLabel DrugsLabel)
        {           
            DrugsLabel dbDrugsLabel = await _DrugsLabelService.UpdateDrugsLabel(DrugsLabel);

            if (dbDrugsLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DrugsLabel.TbDrugsLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDrugsLabel(DrugsLabel DrugsLabel)
        {            
            (bool status, string message) = await _DrugsLabelService.DeleteDrugsLabel(DrugsLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DrugsLabel);
        }
    }
}
