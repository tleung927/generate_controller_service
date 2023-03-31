using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosUfController : ControllerBase
    {
        private readonly IPosUfService _PosUfService;

        public PosUfController(IPosUfService PosUfService)
        {
            _PosUfService = PosUfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosUfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PosUfs = await _PosUfService.GetPosUfListByValue(offset, limit, val);

            if (PosUfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PosUfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, PosUfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosUfList(string PosUf_name)
        {
            var PosUfs = await _PosUfService.GetPosUfList(PosUf_name);

            if (PosUfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosUf found for uci: {PosUf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosUfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosUf(string PosUf_name)
        {
            var PosUfs = await _PosUfService.GetPosUf(PosUf_name);

            if (PosUfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosUf found for uci: {PosUf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosUfs);
        }

        [HttpPost]
        public async Task<ActionResult<PosUf>> AddPosUf(PosUf PosUf)
        {
            var dbPosUf = await _PosUfService.AddPosUf(PosUf);

            if (dbPosUf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosUf.TbPosUfName} could not be added."
                );
            }

            return CreatedAtAction("GetPosUf", new { uci = PosUf.TbPosUfName }, PosUf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePosUf(PosUf PosUf)
        {           
            PosUf dbPosUf = await _PosUfService.UpdatePosUf(PosUf);

            if (dbPosUf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosUf.TbPosUfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePosUf(PosUf PosUf)
        {            
            (bool status, string message) = await _PosUfService.DeletePosUf(PosUf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PosUf);
        }
    }
}
