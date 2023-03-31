using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormPermissionAssignController : ControllerBase
    {
        private readonly IFormPermissionAssignService _FormPermissionAssignService;

        public FormPermissionAssignController(IFormPermissionAssignService FormPermissionAssignService)
        {
            _FormPermissionAssignService = FormPermissionAssignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormPermissionAssignList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormPermissionAssigns = await _FormPermissionAssignService.GetFormPermissionAssignListByValue(offset, limit, val);

            if (FormPermissionAssigns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormPermissionAssigns in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormPermissionAssigns);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormPermissionAssignList(string FormPermissionAssign_name)
        {
            var FormPermissionAssigns = await _FormPermissionAssignService.GetFormPermissionAssignList(FormPermissionAssign_name);

            if (FormPermissionAssigns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormPermissionAssign found for uci: {FormPermissionAssign_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormPermissionAssigns);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormPermissionAssign(string FormPermissionAssign_name)
        {
            var FormPermissionAssigns = await _FormPermissionAssignService.GetFormPermissionAssign(FormPermissionAssign_name);

            if (FormPermissionAssigns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormPermissionAssign found for uci: {FormPermissionAssign_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormPermissionAssigns);
        }

        [HttpPost]
        public async Task<ActionResult<FormPermissionAssign>> AddFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {
            var dbFormPermissionAssign = await _FormPermissionAssignService.AddFormPermissionAssign(FormPermissionAssign);

            if (dbFormPermissionAssign == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormPermissionAssign.TbFormPermissionAssignName} could not be added."
                );
            }

            return CreatedAtAction("GetFormPermissionAssign", new { uci = FormPermissionAssign.TbFormPermissionAssignName }, FormPermissionAssign);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {           
            FormPermissionAssign dbFormPermissionAssign = await _FormPermissionAssignService.UpdateFormPermissionAssign(FormPermissionAssign);

            if (dbFormPermissionAssign == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormPermissionAssign.TbFormPermissionAssignName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {            
            (bool status, string message) = await _FormPermissionAssignService.DeleteFormPermissionAssign(FormPermissionAssign);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormPermissionAssign);
        }
    }
}
