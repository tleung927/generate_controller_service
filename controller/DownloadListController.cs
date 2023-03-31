using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadListController : ControllerBase
    {
        private readonly IDownloadListService _DownloadListService;

        public DownloadListController(IDownloadListService DownloadListService)
        {
            _DownloadListService = DownloadListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloadListList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DownloadLists = await _DownloadListService.GetDownloadListListByValue(offset, limit, val);

            if (DownloadLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DownloadLists in database");
            }

            return StatusCode(StatusCodes.Status200OK, DownloadLists);
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloadListList(string DownloadList_name)
        {
            var DownloadLists = await _DownloadListService.GetDownloadListList(DownloadList_name);

            if (DownloadLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DownloadList found for uci: {DownloadList_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DownloadLists);
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloadList(string DownloadList_name)
        {
            var DownloadLists = await _DownloadListService.GetDownloadList(DownloadList_name);

            if (DownloadLists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DownloadList found for uci: {DownloadList_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DownloadLists);
        }

        [HttpPost]
        public async Task<ActionResult<DownloadList>> AddDownloadList(DownloadList DownloadList)
        {
            var dbDownloadList = await _DownloadListService.AddDownloadList(DownloadList);

            if (dbDownloadList == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DownloadList.TbDownloadListName} could not be added."
                );
            }

            return CreatedAtAction("GetDownloadList", new { uci = DownloadList.TbDownloadListName }, DownloadList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDownloadList(DownloadList DownloadList)
        {           
            DownloadList dbDownloadList = await _DownloadListService.UpdateDownloadList(DownloadList);

            if (dbDownloadList == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DownloadList.TbDownloadListName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDownloadList(DownloadList DownloadList)
        {            
            (bool status, string message) = await _DownloadListService.DeleteDownloadList(DownloadList);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DownloadList);
        }
    }
}
