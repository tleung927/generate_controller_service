using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormTransitionController : ControllerBase
    {
        private readonly IFormTransitionService _FormTransitionService;

        public FormTransitionController(IFormTransitionService FormTransitionService)
        {
            _FormTransitionService = FormTransitionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransitionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormTransitions = await _FormTransitionService.GetFormTransitionListByValue(offset, limit, val);

            if (FormTransitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormTransitions in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransitions);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransitionList(string FormTransition_name)
        {
            var FormTransitions = await _FormTransitionService.GetFormTransitionList(FormTransition_name);

            if (FormTransitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransition found for uci: {FormTransition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransitions);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransition(string FormTransition_name)
        {
            var FormTransitions = await _FormTransitionService.GetFormTransition(FormTransition_name);

            if (FormTransitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransition found for uci: {FormTransition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransitions);
        }

        [HttpPost]
        public async Task<ActionResult<FormTransition>> AddFormTransition(FormTransition FormTransition)
        {
            var dbFormTransition = await _FormTransitionService.AddFormTransition(FormTransition);

            if (dbFormTransition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransition.TbFormTransitionName} could not be added."
                );
            }

            return CreatedAtAction("GetFormTransition", new { uci = FormTransition.TbFormTransitionName }, FormTransition);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormTransition(FormTransition FormTransition)
        {           
            FormTransition dbFormTransition = await _FormTransitionService.UpdateFormTransition(FormTransition);

            if (dbFormTransition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransition.TbFormTransitionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormTransition(FormTransition FormTransition)
        {            
            (bool status, string message) = await _FormTransitionService.DeleteFormTransition(FormTransition);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormTransition);
        }
    }
}
