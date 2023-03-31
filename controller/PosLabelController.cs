using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosLabelController : ControllerBase
    {
        private readonly IPosLabelService _PosLabelService;

        public PosLabelController(IPosLabelService PosLabelService)
        {
            _PosLabelService = PosLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PosLabels = await _PosLabelService.GetPosLabelListByValue(offset, limit, val);

            if (PosLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PosLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, PosLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosLabelList(string PosLabel_name)
        {
            var PosLabels = await _PosLabelService.GetPosLabelList(PosLabel_name);

            if (PosLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosLabel found for uci: {PosLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosLabel(string PosLabel_name)
        {
            var PosLabels = await _PosLabelService.GetPosLabel(PosLabel_name);

            if (PosLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosLabel found for uci: {PosLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosLabels);
        }

        [HttpPost]
        public async Task<ActionResult<PosLabel>> AddPosLabel(PosLabel PosLabel)
        {
            var dbPosLabel = await _PosLabelService.AddPosLabel(PosLabel);

            if (dbPosLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosLabel.TbPosLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetPosLabel", new { uci = PosLabel.TbPosLabelName }, PosLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePosLabel(PosLabel PosLabel)
        {           
            PosLabel dbPosLabel = await _PosLabelService.UpdatePosLabel(PosLabel);

            if (dbPosLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosLabel.TbPosLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePosLabel(PosLabel PosLabel)
        {            
            (bool status, string message) = await _PosLabelService.DeletePosLabel(PosLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PosLabel);
        }
    }
}
