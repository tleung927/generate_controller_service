using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdnotesLabelController : ControllerBase
    {
        private readonly IIdnotesLabelService _IdnotesLabelService;

        public IdnotesLabelController(IIdnotesLabelService IdnotesLabelService)
        {
            _IdnotesLabelService = IdnotesLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIdnotesLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var IdnotesLabels = await _IdnotesLabelService.GetIdnotesLabelListByValue(offset, limit, val);

            if (IdnotesLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No IdnotesLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, IdnotesLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdnotesLabelList(string IdnotesLabel_name)
        {
            var IdnotesLabels = await _IdnotesLabelService.GetIdnotesLabelList(IdnotesLabel_name);

            if (IdnotesLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No IdnotesLabel found for uci: {IdnotesLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, IdnotesLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdnotesLabel(string IdnotesLabel_name)
        {
            var IdnotesLabels = await _IdnotesLabelService.GetIdnotesLabel(IdnotesLabel_name);

            if (IdnotesLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No IdnotesLabel found for uci: {IdnotesLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, IdnotesLabels);
        }

        [HttpPost]
        public async Task<ActionResult<IdnotesLabel>> AddIdnotesLabel(IdnotesLabel IdnotesLabel)
        {
            var dbIdnotesLabel = await _IdnotesLabelService.AddIdnotesLabel(IdnotesLabel);

            if (dbIdnotesLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{IdnotesLabel.TbIdnotesLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetIdnotesLabel", new { uci = IdnotesLabel.TbIdnotesLabelName }, IdnotesLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIdnotesLabel(IdnotesLabel IdnotesLabel)
        {           
            IdnotesLabel dbIdnotesLabel = await _IdnotesLabelService.UpdateIdnotesLabel(IdnotesLabel);

            if (dbIdnotesLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{IdnotesLabel.TbIdnotesLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIdnotesLabel(IdnotesLabel IdnotesLabel)
        {            
            (bool status, string message) = await _IdnotesLabelService.DeleteIdnotesLabel(IdnotesLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, IdnotesLabel);
        }
    }
}
