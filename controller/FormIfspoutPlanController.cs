using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspoutPlanController : ControllerBase
    {
        private readonly IFormIfspoutPlanService _FormIfspoutPlanService;

        public FormIfspoutPlanController(IFormIfspoutPlanService FormIfspoutPlanService)
        {
            _FormIfspoutPlanService = FormIfspoutPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspoutPlanList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspoutPlans = await _FormIfspoutPlanService.GetFormIfspoutPlanListByValue(offset, limit, val);

            if (FormIfspoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspoutPlans in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspoutPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspoutPlanList(string FormIfspoutPlan_name)
        {
            var FormIfspoutPlans = await _FormIfspoutPlanService.GetFormIfspoutPlanList(FormIfspoutPlan_name);

            if (FormIfspoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspoutPlan found for uci: {FormIfspoutPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspoutPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspoutPlan(string FormIfspoutPlan_name)
        {
            var FormIfspoutPlans = await _FormIfspoutPlanService.GetFormIfspoutPlan(FormIfspoutPlan_name);

            if (FormIfspoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspoutPlan found for uci: {FormIfspoutPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspoutPlans);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspoutPlan>> AddFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {
            var dbFormIfspoutPlan = await _FormIfspoutPlanService.AddFormIfspoutPlan(FormIfspoutPlan);

            if (dbFormIfspoutPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspoutPlan.TbFormIfspoutPlanName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspoutPlan", new { uci = FormIfspoutPlan.TbFormIfspoutPlanName }, FormIfspoutPlan);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {           
            FormIfspoutPlan dbFormIfspoutPlan = await _FormIfspoutPlanService.UpdateFormIfspoutPlan(FormIfspoutPlan);

            if (dbFormIfspoutPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspoutPlan.TbFormIfspoutPlanName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {            
            (bool status, string message) = await _FormIfspoutPlanService.DeleteFormIfspoutPlan(FormIfspoutPlan);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspoutPlan);
        }
    }
}
