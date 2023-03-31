using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DsmcodpfController : ControllerBase
    {
        private readonly IDsmcodpfService _DsmcodpfService;

        public DsmcodpfController(IDsmcodpfService DsmcodpfService)
        {
            _DsmcodpfService = DsmcodpfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDsmcodpfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Dsmcodpfs = await _DsmcodpfService.GetDsmcodpfListByValue(offset, limit, val);

            if (Dsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Dsmcodpfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Dsmcodpfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDsmcodpfList(string Dsmcodpf_name)
        {
            var Dsmcodpfs = await _DsmcodpfService.GetDsmcodpfList(Dsmcodpf_name);

            if (Dsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Dsmcodpf found for uci: {Dsmcodpf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Dsmcodpfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDsmcodpf(string Dsmcodpf_name)
        {
            var Dsmcodpfs = await _DsmcodpfService.GetDsmcodpf(Dsmcodpf_name);

            if (Dsmcodpfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Dsmcodpf found for uci: {Dsmcodpf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Dsmcodpfs);
        }

        [HttpPost]
        public async Task<ActionResult<Dsmcodpf>> AddDsmcodpf(Dsmcodpf Dsmcodpf)
        {
            var dbDsmcodpf = await _DsmcodpfService.AddDsmcodpf(Dsmcodpf);

            if (dbDsmcodpf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Dsmcodpf.TbDsmcodpfName} could not be added."
                );
            }

            return CreatedAtAction("GetDsmcodpf", new { uci = Dsmcodpf.TbDsmcodpfName }, Dsmcodpf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDsmcodpf(Dsmcodpf Dsmcodpf)
        {           
            Dsmcodpf dbDsmcodpf = await _DsmcodpfService.UpdateDsmcodpf(Dsmcodpf);

            if (dbDsmcodpf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Dsmcodpf.TbDsmcodpfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDsmcodpf(Dsmcodpf Dsmcodpf)
        {            
            (bool status, string message) = await _DsmcodpfService.DeleteDsmcodpf(Dsmcodpf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Dsmcodpf);
        }
    }
}
