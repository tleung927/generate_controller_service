using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewEsEligibleConditionController : ControllerBase
    {
        private readonly IViewEsEligibleConditionService _ViewEsEligibleConditionService;

        public ViewEsEligibleConditionController(IViewEsEligibleConditionService ViewEsEligibleConditionService)
        {
            _ViewEsEligibleConditionService = ViewEsEligibleConditionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewEsEligibleConditionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewEsEligibleConditions = await _ViewEsEligibleConditionService.GetViewEsEligibleConditionListByValue(offset, limit, val);

            if (ViewEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewEsEligibleConditions in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewEsEligibleConditions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewEsEligibleConditionList(string ViewEsEligibleCondition_name)
        {
            var ViewEsEligibleConditions = await _ViewEsEligibleConditionService.GetViewEsEligibleConditionList(ViewEsEligibleCondition_name);

            if (ViewEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewEsEligibleCondition found for uci: {ViewEsEligibleCondition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewEsEligibleConditions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewEsEligibleCondition(string ViewEsEligibleCondition_name)
        {
            var ViewEsEligibleConditions = await _ViewEsEligibleConditionService.GetViewEsEligibleCondition(ViewEsEligibleCondition_name);

            if (ViewEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewEsEligibleCondition found for uci: {ViewEsEligibleCondition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewEsEligibleConditions);
        }

        [HttpPost]
        public async Task<ActionResult<ViewEsEligibleCondition>> AddViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {
            var dbViewEsEligibleCondition = await _ViewEsEligibleConditionService.AddViewEsEligibleCondition(ViewEsEligibleCondition);

            if (dbViewEsEligibleCondition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewEsEligibleCondition.TbViewEsEligibleConditionName} could not be added."
                );
            }

            return CreatedAtAction("GetViewEsEligibleCondition", new { uci = ViewEsEligibleCondition.TbViewEsEligibleConditionName }, ViewEsEligibleCondition);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {           
            ViewEsEligibleCondition dbViewEsEligibleCondition = await _ViewEsEligibleConditionService.UpdateViewEsEligibleCondition(ViewEsEligibleCondition);

            if (dbViewEsEligibleCondition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewEsEligibleCondition.TbViewEsEligibleConditionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {            
            (bool status, string message) = await _ViewEsEligibleConditionService.DeleteViewEsEligibleCondition(ViewEsEligibleCondition);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewEsEligibleCondition);
        }
    }
}
