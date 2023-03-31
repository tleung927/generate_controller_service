using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RptCaseMgnPlanController : ControllerBase
    {
        private readonly IRptCaseMgnPlanService _RptCaseMgnPlanService;

        public RptCaseMgnPlanController(IRptCaseMgnPlanService RptCaseMgnPlanService)
        {
            _RptCaseMgnPlanService = RptCaseMgnPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRptCaseMgnPlanList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var RptCaseMgnPlans = await _RptCaseMgnPlanService.GetRptCaseMgnPlanListByValue(offset, limit, val);

            if (RptCaseMgnPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No RptCaseMgnPlans in database");
            }

            return StatusCode(StatusCodes.Status200OK, RptCaseMgnPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetRptCaseMgnPlanList(string RptCaseMgnPlan_name)
        {
            var RptCaseMgnPlans = await _RptCaseMgnPlanService.GetRptCaseMgnPlanList(RptCaseMgnPlan_name);

            if (RptCaseMgnPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RptCaseMgnPlan found for uci: {RptCaseMgnPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RptCaseMgnPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetRptCaseMgnPlan(string RptCaseMgnPlan_name)
        {
            var RptCaseMgnPlans = await _RptCaseMgnPlanService.GetRptCaseMgnPlan(RptCaseMgnPlan_name);

            if (RptCaseMgnPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RptCaseMgnPlan found for uci: {RptCaseMgnPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RptCaseMgnPlans);
        }

        [HttpPost]
        public async Task<ActionResult<RptCaseMgnPlan>> AddRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {
            var dbRptCaseMgnPlan = await _RptCaseMgnPlanService.AddRptCaseMgnPlan(RptCaseMgnPlan);

            if (dbRptCaseMgnPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RptCaseMgnPlan.TbRptCaseMgnPlanName} could not be added."
                );
            }

            return CreatedAtAction("GetRptCaseMgnPlan", new { uci = RptCaseMgnPlan.TbRptCaseMgnPlanName }, RptCaseMgnPlan);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {           
            RptCaseMgnPlan dbRptCaseMgnPlan = await _RptCaseMgnPlanService.UpdateRptCaseMgnPlan(RptCaseMgnPlan);

            if (dbRptCaseMgnPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RptCaseMgnPlan.TbRptCaseMgnPlanName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {            
            (bool status, string message) = await _RptCaseMgnPlanService.DeleteRptCaseMgnPlan(RptCaseMgnPlan);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, RptCaseMgnPlan);
        }
    }
}
