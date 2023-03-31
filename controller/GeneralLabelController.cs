using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralLabelController : ControllerBase
    {
        private readonly IGeneralLabelService _GeneralLabelService;

        public GeneralLabelController(IGeneralLabelService GeneralLabelService)
        {
            _GeneralLabelService = GeneralLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GeneralLabels = await _GeneralLabelService.GetGeneralLabelListByValue(offset, limit, val);

            if (GeneralLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GeneralLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralLabelList(string GeneralLabel_name)
        {
            var GeneralLabels = await _GeneralLabelService.GetGeneralLabelList(GeneralLabel_name);

            if (GeneralLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralLabel found for uci: {GeneralLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralLabel(string GeneralLabel_name)
        {
            var GeneralLabels = await _GeneralLabelService.GetGeneralLabel(GeneralLabel_name);

            if (GeneralLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralLabel found for uci: {GeneralLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralLabels);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralLabel>> AddGeneralLabel(GeneralLabel GeneralLabel)
        {
            var dbGeneralLabel = await _GeneralLabelService.AddGeneralLabel(GeneralLabel);

            if (dbGeneralLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralLabel.TbGeneralLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetGeneralLabel", new { uci = GeneralLabel.TbGeneralLabelName }, GeneralLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralLabel(GeneralLabel GeneralLabel)
        {           
            GeneralLabel dbGeneralLabel = await _GeneralLabelService.UpdateGeneralLabel(GeneralLabel);

            if (dbGeneralLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralLabel.TbGeneralLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGeneralLabel(GeneralLabel GeneralLabel)
        {            
            (bool status, string message) = await _GeneralLabelService.DeleteGeneralLabel(GeneralLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GeneralLabel);
        }
    }
}
