using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebLinkController : ControllerBase
    {
        private readonly IWebLinkService _WebLinkService;

        public WebLinkController(IWebLinkService WebLinkService)
        {
            _WebLinkService = WebLinkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWebLinkList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var WebLinks = await _WebLinkService.GetWebLinkListByValue(offset, limit, val);

            if (WebLinks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No WebLinks in database");
            }

            return StatusCode(StatusCodes.Status200OK, WebLinks);
        }

        [HttpGet]
        public async Task<IActionResult> GetWebLinkList(string WebLink_name)
        {
            var WebLinks = await _WebLinkService.GetWebLinkList(WebLink_name);

            if (WebLinks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No WebLink found for uci: {WebLink_name}");
            }

            return StatusCode(StatusCodes.Status200OK, WebLinks);
        }

        [HttpGet]
        public async Task<IActionResult> GetWebLink(string WebLink_name)
        {
            var WebLinks = await _WebLinkService.GetWebLink(WebLink_name);

            if (WebLinks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No WebLink found for uci: {WebLink_name}");
            }

            return StatusCode(StatusCodes.Status200OK, WebLinks);
        }

        [HttpPost]
        public async Task<ActionResult<WebLink>> AddWebLink(WebLink WebLink)
        {
            var dbWebLink = await _WebLinkService.AddWebLink(WebLink);

            if (dbWebLink == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{WebLink.TbWebLinkName} could not be added."
                );
            }

            return CreatedAtAction("GetWebLink", new { uci = WebLink.TbWebLinkName }, WebLink);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWebLink(WebLink WebLink)
        {           
            WebLink dbWebLink = await _WebLinkService.UpdateWebLink(WebLink);

            if (dbWebLink == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{WebLink.TbWebLinkName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWebLink(WebLink WebLink)
        {            
            (bool status, string message) = await _WebLinkService.DeleteWebLink(WebLink);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, WebLink);
        }
    }
}
