using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderModifyController : ControllerBase
    {
        private readonly ICderModifyService _CderModifyService;

        public CderModifyController(ICderModifyService CderModifyService)
        {
            _CderModifyService = CderModifyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderModifyList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CderModifys = await _CderModifyService.GetCderModifyListByValue(offset, limit, val);

            if (CderModifys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CderModifys in database");
            }

            return StatusCode(StatusCodes.Status200OK, CderModifys);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderModifyList(string CderModify_name)
        {
            var CderModifys = await _CderModifyService.GetCderModifyList(CderModify_name);

            if (CderModifys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderModify found for uci: {CderModify_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderModifys);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderModify(string CderModify_name)
        {
            var CderModifys = await _CderModifyService.GetCderModify(CderModify_name);

            if (CderModifys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CderModify found for uci: {CderModify_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CderModifys);
        }

        [HttpPost]
        public async Task<ActionResult<CderModify>> AddCderModify(CderModify CderModify)
        {
            var dbCderModify = await _CderModifyService.AddCderModify(CderModify);

            if (dbCderModify == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderModify.TbCderModifyName} could not be added."
                );
            }

            return CreatedAtAction("GetCderModify", new { uci = CderModify.TbCderModifyName }, CderModify);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCderModify(CderModify CderModify)
        {           
            CderModify dbCderModify = await _CderModifyService.UpdateCderModify(CderModify);

            if (dbCderModify == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CderModify.TbCderModifyName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCderModify(CderModify CderModify)
        {            
            (bool status, string message) = await _CderModifyService.DeleteCderModify(CderModify);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CderModify);
        }
    }
}
