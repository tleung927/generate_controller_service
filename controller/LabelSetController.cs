using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelSetController : ControllerBase
    {
        private readonly ILabelSetService _LabelSetService;

        public LabelSetController(ILabelSetService LabelSetService)
        {
            _LabelSetService = LabelSetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLabelSetList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LabelSets = await _LabelSetService.GetLabelSetListByValue(offset, limit, val);

            if (LabelSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LabelSets in database");
            }

            return StatusCode(StatusCodes.Status200OK, LabelSets);
        }

        [HttpGet]
        public async Task<IActionResult> GetLabelSetList(string LabelSet_name)
        {
            var LabelSets = await _LabelSetService.GetLabelSetList(LabelSet_name);

            if (LabelSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LabelSet found for uci: {LabelSet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LabelSets);
        }

        [HttpGet]
        public async Task<IActionResult> GetLabelSet(string LabelSet_name)
        {
            var LabelSets = await _LabelSetService.GetLabelSet(LabelSet_name);

            if (LabelSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LabelSet found for uci: {LabelSet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LabelSets);
        }

        [HttpPost]
        public async Task<ActionResult<LabelSet>> AddLabelSet(LabelSet LabelSet)
        {
            var dbLabelSet = await _LabelSetService.AddLabelSet(LabelSet);

            if (dbLabelSet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LabelSet.TbLabelSetName} could not be added."
                );
            }

            return CreatedAtAction("GetLabelSet", new { uci = LabelSet.TbLabelSetName }, LabelSet);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLabelSet(LabelSet LabelSet)
        {           
            LabelSet dbLabelSet = await _LabelSetService.UpdateLabelSet(LabelSet);

            if (dbLabelSet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LabelSet.TbLabelSetName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLabelSet(LabelSet LabelSet)
        {            
            (bool status, string message) = await _LabelSetService.DeleteLabelSet(LabelSet);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LabelSet);
        }
    }
}
