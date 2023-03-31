using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _SearchService;

        public SearchController(ISearchService SearchService)
        {
            _SearchService = SearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Searchs = await _SearchService.GetSearchListByValue(offset, limit, val);

            if (Searchs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Searchs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Searchs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchList(string Search_name)
        {
            var Searchs = await _SearchService.GetSearchList(Search_name);

            if (Searchs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Search found for uci: {Search_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Searchs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSearch(string Search_name)
        {
            var Searchs = await _SearchService.GetSearch(Search_name);

            if (Searchs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Search found for uci: {Search_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Searchs);
        }

        [HttpPost]
        public async Task<ActionResult<Search>> AddSearch(Search Search)
        {
            var dbSearch = await _SearchService.AddSearch(Search);

            if (dbSearch == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Search.TbSearchName} could not be added."
                );
            }

            return CreatedAtAction("GetSearch", new { uci = Search.TbSearchName }, Search);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSearch(Search Search)
        {           
            Search dbSearch = await _SearchService.UpdateSearch(Search);

            if (dbSearch == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Search.TbSearchName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSearch(Search Search)
        {            
            (bool status, string message) = await _SearchService.DeleteSearch(Search);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Search);
        }
    }
}
