using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleControllerCategoryController : ControllerBase
    {
        private readonly IViewScheduleControllerCategoryService _ViewScheduleControllerCategoryService;

        public ViewScheduleControllerCategoryController(IViewScheduleControllerCategoryService ViewScheduleControllerCategoryService)
        {
            _ViewScheduleControllerCategoryService = ViewScheduleControllerCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerCategoryList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleControllerCategorys = await _ViewScheduleControllerCategoryService.GetViewScheduleControllerCategoryListByValue(offset, limit, val);

            if (ViewScheduleControllerCategorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleControllerCategorys in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerCategorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerCategoryList(string ViewScheduleControllerCategory_name)
        {
            var ViewScheduleControllerCategorys = await _ViewScheduleControllerCategoryService.GetViewScheduleControllerCategoryList(ViewScheduleControllerCategory_name);

            if (ViewScheduleControllerCategorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleControllerCategory found for uci: {ViewScheduleControllerCategory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerCategorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerCategory(string ViewScheduleControllerCategory_name)
        {
            var ViewScheduleControllerCategorys = await _ViewScheduleControllerCategoryService.GetViewScheduleControllerCategory(ViewScheduleControllerCategory_name);

            if (ViewScheduleControllerCategorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleControllerCategory found for uci: {ViewScheduleControllerCategory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerCategorys);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleControllerCategory>> AddViewScheduleControllerCategory(ViewScheduleControllerCategory ViewScheduleControllerCategory)
        {
            var dbViewScheduleControllerCategory = await _ViewScheduleControllerCategoryService.AddViewScheduleControllerCategory(ViewScheduleControllerCategory);

            if (dbViewScheduleControllerCategory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleControllerCategory.TbViewScheduleControllerCategoryName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleControllerCategory", new { uci = ViewScheduleControllerCategory.TbViewScheduleControllerCategoryName }, ViewScheduleControllerCategory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleControllerCategory(ViewScheduleControllerCategory ViewScheduleControllerCategory)
        {           
            ViewScheduleControllerCategory dbViewScheduleControllerCategory = await _ViewScheduleControllerCategoryService.UpdateViewScheduleControllerCategory(ViewScheduleControllerCategory);

            if (dbViewScheduleControllerCategory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleControllerCategory.TbViewScheduleControllerCategoryName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleControllerCategory(ViewScheduleControllerCategory ViewScheduleControllerCategory)
        {            
            (bool status, string message) = await _ViewScheduleControllerCategoryService.DeleteViewScheduleControllerCategory(ViewScheduleControllerCategory);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerCategory);
        }
    }
}
