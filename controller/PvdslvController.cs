using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PvdslvController : ControllerBase
    {
        private readonly IPvdslvService _PvdslvService;

        public PvdslvController(IPvdslvService PvdslvService)
        {
            _PvdslvService = PvdslvService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPvdslvList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pvdslvs = await _PvdslvService.GetPvdslvListByValue(offset, limit, val);

            if (Pvdslvs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pvdslvs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pvdslvs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPvdslvList(string Pvdslv_name)
        {
            var Pvdslvs = await _PvdslvService.GetPvdslvList(Pvdslv_name);

            if (Pvdslvs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pvdslv found for uci: {Pvdslv_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pvdslvs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPvdslv(string Pvdslv_name)
        {
            var Pvdslvs = await _PvdslvService.GetPvdslv(Pvdslv_name);

            if (Pvdslvs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pvdslv found for uci: {Pvdslv_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pvdslvs);
        }

        [HttpPost]
        public async Task<ActionResult<Pvdslv>> AddPvdslv(Pvdslv Pvdslv)
        {
            var dbPvdslv = await _PvdslvService.AddPvdslv(Pvdslv);

            if (dbPvdslv == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pvdslv.TbPvdslvName} could not be added."
                );
            }

            return CreatedAtAction("GetPvdslv", new { uci = Pvdslv.TbPvdslvName }, Pvdslv);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePvdslv(Pvdslv Pvdslv)
        {           
            Pvdslv dbPvdslv = await _PvdslvService.UpdatePvdslv(Pvdslv);

            if (dbPvdslv == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pvdslv.TbPvdslvName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePvdslv(Pvdslv Pvdslv)
        {            
            (bool status, string message) = await _PvdslvService.DeletePvdslv(Pvdslv);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pvdslv);
        }
    }
}
