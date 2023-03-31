using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewDsmcodpfController : ControllerBase
    {
        private readonly IViewDsmcodpfService _ViewDsmcodpfService;

        public ViewDsmcodpfController(IViewDsmcodpfService ViewDsmcodpfService)
        {
            _ViewDsmcodpfService = ViewDsmcodpfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDsmcodpfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewDsmcodpfs = await _ViewDsmcodpfService.GetViewDsmcodpfListByValue(offset, limit, val);

            if (ViewDsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewDsmcodpfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDsmcodpfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDsmcodpfList(string ViewDsmcodpf_name)
        {
            var ViewDsmcodpfs = await _ViewDsmcodpfService.GetViewDsmcodpfList(ViewDsmcodpf_name);

            if (ViewDsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewDsmcodpf found for uci: {ViewDsmcodpf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDsmcodpfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDsmcodpf(string ViewDsmcodpf_name)
        {
            var ViewDsmcodpfs = await _ViewDsmcodpfService.GetViewDsmcodpf(ViewDsmcodpf_name);

            if (ViewDsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewDsmcodpf found for uci: {ViewDsmcodpf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDsmcodpfs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewDsmcodpf>> AddViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {
            var dbViewDsmcodpf = await _ViewDsmcodpfService.AddViewDsmcodpf(ViewDsmcodpf);

            if (dbViewDsmcodpf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewDsmcodpf.TbViewDsmcodpfName} could not be added."
                );
            }

            return CreatedAtAction("GetViewDsmcodpf", new { uci = ViewDsmcodpf.TbViewDsmcodpfName }, ViewDsmcodpf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {           
            ViewDsmcodpf dbViewDsmcodpf = await _ViewDsmcodpfService.UpdateViewDsmcodpf(ViewDsmcodpf);

            if (dbViewDsmcodpf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewDsmcodpf.TbViewDsmcodpfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {            
            (bool status, string message) = await _ViewDsmcodpfService.DeleteViewDsmcodpf(ViewDsmcodpf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewDsmcodpf);
        }
    }
}
