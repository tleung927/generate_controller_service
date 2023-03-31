using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppplanGroupController : ControllerBase
    {
        private readonly IFormIppplanGroupService _FormIppplanGroupService;

        public FormIppplanGroupController(IFormIppplanGroupService FormIppplanGroupService)
        {
            _FormIppplanGroupService = FormIppplanGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppplanGroups = await _FormIppplanGroupService.GetFormIppplanGroupListByValue(offset, limit, val);

            if (FormIppplanGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppplanGroups in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroups);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroupList(string FormIppplanGroup_name)
        {
            var FormIppplanGroups = await _FormIppplanGroupService.GetFormIppplanGroupList(FormIppplanGroup_name);

            if (FormIppplanGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanGroup found for uci: {FormIppplanGroup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroups);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroup(string FormIppplanGroup_name)
        {
            var FormIppplanGroups = await _FormIppplanGroupService.GetFormIppplanGroup(FormIppplanGroup_name);

            if (FormIppplanGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanGroup found for uci: {FormIppplanGroup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroups);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppplanGroup>> AddFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {
            var dbFormIppplanGroup = await _FormIppplanGroupService.AddFormIppplanGroup(FormIppplanGroup);

            if (dbFormIppplanGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanGroup.TbFormIppplanGroupName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppplanGroup", new { uci = FormIppplanGroup.TbFormIppplanGroupName }, FormIppplanGroup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {           
            FormIppplanGroup dbFormIppplanGroup = await _FormIppplanGroupService.UpdateFormIppplanGroup(FormIppplanGroup);

            if (dbFormIppplanGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanGroup.TbFormIppplanGroupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {            
            (bool status, string message) = await _FormIppplanGroupService.DeleteFormIppplanGroup(FormIppplanGroup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroup);
        }
    }
}
