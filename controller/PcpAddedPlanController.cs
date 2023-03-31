using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PcpAddedPlanController : ControllerBase
    {
        private readonly IPcpAddedPlanService _PcpAddedPlanService;

        public PcpAddedPlanController(IPcpAddedPlanService PcpAddedPlanService)
        {
            _PcpAddedPlanService = PcpAddedPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpAddedPlanList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PcpAddedPlans = await _PcpAddedPlanService.GetPcpAddedPlanListByValue(offset, limit, val);

            if (PcpAddedPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PcpAddedPlans in database");
            }

            return StatusCode(StatusCodes.Status200OK, PcpAddedPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpAddedPlanList(string PcpAddedPlan_name)
        {
            var PcpAddedPlans = await _PcpAddedPlanService.GetPcpAddedPlanList(PcpAddedPlan_name);

            if (PcpAddedPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpAddedPlan found for uci: {PcpAddedPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpAddedPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpAddedPlan(string PcpAddedPlan_name)
        {
            var PcpAddedPlans = await _PcpAddedPlanService.GetPcpAddedPlan(PcpAddedPlan_name);

            if (PcpAddedPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpAddedPlan found for uci: {PcpAddedPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpAddedPlans);
        }

        [HttpPost]
        public async Task<ActionResult<PcpAddedPlan>> AddPcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {
            var dbPcpAddedPlan = await _PcpAddedPlanService.AddPcpAddedPlan(PcpAddedPlan);

            if (dbPcpAddedPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpAddedPlan.TbPcpAddedPlanName} could not be added."
                );
            }

            return CreatedAtAction("GetPcpAddedPlan", new { uci = PcpAddedPlan.TbPcpAddedPlanName }, PcpAddedPlan);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {           
            PcpAddedPlan dbPcpAddedPlan = await _PcpAddedPlanService.UpdatePcpAddedPlan(PcpAddedPlan);

            if (dbPcpAddedPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpAddedPlan.TbPcpAddedPlanName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {            
            (bool status, string message) = await _PcpAddedPlanService.DeletePcpAddedPlan(PcpAddedPlan);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PcpAddedPlan);
        }
    }
}
