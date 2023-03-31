using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrformListController : ControllerBase
    {
        private readonly ICrformListService _CrformListService;

        public CrformListController(ICrformListService CrformListService)
        {
            _CrformListService = CrformListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCrformListList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var CrformLists = await _CrformListService.GetCrformListListByValue(offset, limit, val);

            if (CrformLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No CrformLists in database");
            }

            return StatusCode(StatusCodes.Status200OK, CrformLists);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrformListList(string CrformList_name)
        {
            var CrformLists = await _CrformListService.GetCrformListList(CrformList_name);

            if (CrformLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CrformList found for uci: {CrformList_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CrformLists);
        }

        [HttpGet]
        public async Task<IActionResult> GetCrformList(string CrformList_name)
        {
            var CrformLists = await _CrformListService.GetCrformList(CrformList_name);

            if (CrformLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No CrformList found for uci: {CrformList_name}");
            }

            return StatusCode(StatusCodes.Status200OK, CrformLists);
        }

        [HttpPost]
        public async Task<ActionResult<CrformList>> AddCrformList(CrformList CrformList)
        {
            var dbCrformList = await _CrformListService.AddCrformList(CrformList);

            if (dbCrformList == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CrformList.TbCrformListName} could not be added."
                );
            }

            return CreatedAtAction("GetCrformList", new { uci = CrformList.TbCrformListName }, CrformList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCrformList(CrformList CrformList)
        {           
            CrformList dbCrformList = await _CrformListService.UpdateCrformList(CrformList);

            if (dbCrformList == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{CrformList.TbCrformListName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCrformList(CrformList CrformList)
        {            
            (bool status, string message) = await _CrformListService.DeleteCrformList(CrformList);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, CrformList);
        }
    }
}
