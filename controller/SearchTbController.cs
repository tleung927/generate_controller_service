using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchTbController : ControllerBase
    {
        private readonly ISearchTbService _SearchTbService;

        public SearchTbController(ISearchTbService SearchTbService)
        {
            _SearchTbService = SearchTbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchTbList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SearchTbs = await _SearchTbService.GetSearchTbListByValue(offset, limit, val);

            if (SearchTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SearchTbs in database");
            }

            return StatusCode(StatusCodes.Status200OK, SearchTbs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchTbList(string SearchTb_name)
        {
            var SearchTbs = await _SearchTbService.GetSearchTbList(SearchTb_name);

            if (SearchTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SearchTb found for uci: {SearchTb_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SearchTbs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchTb(string SearchTb_name)
        {
            var SearchTbs = await _SearchTbService.GetSearchTb(SearchTb_name);

            if (SearchTbs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SearchTb found for uci: {SearchTb_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SearchTbs);
        }

        [HttpPost]
        public async Task<ActionResult<SearchTb>> AddSearchTb(SearchTb SearchTb)
        {
            var dbSearchTb = await _SearchTbService.AddSearchTb(SearchTb);

            if (dbSearchTb == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SearchTb.TbSearchTbName} could not be added."
                );
            }

            return CreatedAtAction("GetSearchTb", new { uci = SearchTb.TbSearchTbName }, SearchTb);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSearchTb(SearchTb SearchTb)
        {           
            SearchTb dbSearchTb = await _SearchTbService.UpdateSearchTb(SearchTb);

            if (dbSearchTb == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SearchTb.TbSearchTbName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSearchTb(SearchTb SearchTb)
        {            
            (bool status, string message) = await _SearchTbService.DeleteSearchTb(SearchTb);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SearchTb);
        }
    }
}
