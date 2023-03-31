using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewLicenseTypeController : ControllerBase
    {
        private readonly IViewLicenseTypeService _ViewLicenseTypeService;

        public ViewLicenseTypeController(IViewLicenseTypeService ViewLicenseTypeService)
        {
            _ViewLicenseTypeService = ViewLicenseTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLicenseTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewLicenseTypes = await _ViewLicenseTypeService.GetViewLicenseTypeListByValue(offset, limit, val);

            if (ViewLicenseTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewLicenseTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLicenseTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLicenseTypeList(string ViewLicenseType_name)
        {
            var ViewLicenseTypes = await _ViewLicenseTypeService.GetViewLicenseTypeList(ViewLicenseType_name);

            if (ViewLicenseTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLicenseType found for uci: {ViewLicenseType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLicenseTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLicenseType(string ViewLicenseType_name)
        {
            var ViewLicenseTypes = await _ViewLicenseTypeService.GetViewLicenseType(ViewLicenseType_name);

            if (ViewLicenseTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLicenseType found for uci: {ViewLicenseType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLicenseTypes);
        }

        [HttpPost]
        public async Task<ActionResult<ViewLicenseType>> AddViewLicenseType(ViewLicenseType ViewLicenseType)
        {
            var dbViewLicenseType = await _ViewLicenseTypeService.AddViewLicenseType(ViewLicenseType);

            if (dbViewLicenseType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLicenseType.TbViewLicenseTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetViewLicenseType", new { uci = ViewLicenseType.TbViewLicenseTypeName }, ViewLicenseType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewLicenseType(ViewLicenseType ViewLicenseType)
        {           
            ViewLicenseType dbViewLicenseType = await _ViewLicenseTypeService.UpdateViewLicenseType(ViewLicenseType);

            if (dbViewLicenseType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLicenseType.TbViewLicenseTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewLicenseType(ViewLicenseType ViewLicenseType)
        {            
            (bool status, string message) = await _ViewLicenseTypeService.DeleteViewLicenseType(ViewLicenseType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewLicenseType);
        }
    }
}
