using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleEsEligibleConditionController : ControllerBase
    {
        private readonly IViewScheduleEsEligibleConditionService _ViewScheduleEsEligibleConditionService;

        public ViewScheduleEsEligibleConditionController(IViewScheduleEsEligibleConditionService ViewScheduleEsEligibleConditionService)
        {
            _ViewScheduleEsEligibleConditionService = ViewScheduleEsEligibleConditionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsEligibleConditionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleEsEligibleConditions = await _ViewScheduleEsEligibleConditionService.GetViewScheduleEsEligibleConditionListByValue(offset, limit, val);

            if (ViewScheduleEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleEsEligibleConditions in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsEligibleConditions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsEligibleConditionList(string ViewScheduleEsEligibleCondition_name)
        {
            var ViewScheduleEsEligibleConditions = await _ViewScheduleEsEligibleConditionService.GetViewScheduleEsEligibleConditionList(ViewScheduleEsEligibleCondition_name);

            if (ViewScheduleEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleEsEligibleCondition found for uci: {ViewScheduleEsEligibleCondition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsEligibleConditions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsEligibleCondition(string ViewScheduleEsEligibleCondition_name)
        {
            var ViewScheduleEsEligibleConditions = await _ViewScheduleEsEligibleConditionService.GetViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition_name);

            if (ViewScheduleEsEligibleConditions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleEsEligibleCondition found for uci: {ViewScheduleEsEligibleCondition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsEligibleConditions);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleEsEligibleCondition>> AddViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {
            var dbViewScheduleEsEligibleCondition = await _ViewScheduleEsEligibleConditionService.AddViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition);

            if (dbViewScheduleEsEligibleCondition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleEsEligibleCondition.TbViewScheduleEsEligibleConditionName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleEsEligibleCondition", new { uci = ViewScheduleEsEligibleCondition.TbViewScheduleEsEligibleConditionName }, ViewScheduleEsEligibleCondition);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {           
            ViewScheduleEsEligibleCondition dbViewScheduleEsEligibleCondition = await _ViewScheduleEsEligibleConditionService.UpdateViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition);

            if (dbViewScheduleEsEligibleCondition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleEsEligibleCondition.TbViewScheduleEsEligibleConditionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {            
            (bool status, string message) = await _ViewScheduleEsEligibleConditionService.DeleteViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsEligibleCondition);
        }
    }
}
