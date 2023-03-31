using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppoutPlanController : ControllerBase
    {
        private readonly IFormIppoutPlanService _FormIppoutPlanService;

        public FormIppoutPlanController(IFormIppoutPlanService FormIppoutPlanService)
        {
            _FormIppoutPlanService = FormIppoutPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppoutPlanList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppoutPlans = await _FormIppoutPlanService.GetFormIppoutPlanListByValue(offset, limit, val);

            if (FormIppoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppoutPlans in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppoutPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppoutPlanList(string FormIppoutPlan_name)
        {
            var FormIppoutPlans = await _FormIppoutPlanService.GetFormIppoutPlanList(FormIppoutPlan_name);

            if (FormIppoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppoutPlan found for uci: {FormIppoutPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppoutPlans);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppoutPlan(string FormIppoutPlan_name)
        {
            var FormIppoutPlans = await _FormIppoutPlanService.GetFormIppoutPlan(FormIppoutPlan_name);

            if (FormIppoutPlans == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppoutPlan found for uci: {FormIppoutPlan_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppoutPlans);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppoutPlan>> AddFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {
            var dbFormIppoutPlan = await _FormIppoutPlanService.AddFormIppoutPlan(FormIppoutPlan);

            if (dbFormIppoutPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppoutPlan.TbFormIppoutPlanName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppoutPlan", new { uci = FormIppoutPlan.TbFormIppoutPlanName }, FormIppoutPlan);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {           
            FormIppoutPlan dbFormIppoutPlan = await _FormIppoutPlanService.UpdateFormIppoutPlan(FormIppoutPlan);

            if (dbFormIppoutPlan == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppoutPlan.TbFormIppoutPlanName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {            
            (bool status, string message) = await _FormIppoutPlanService.DeleteFormIppoutPlan(FormIppoutPlan);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppoutPlan);
        }
    }
}
