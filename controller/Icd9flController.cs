using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Icd9flController : ControllerBase
    {
        private readonly IIcd9flService _Icd9flService;

        public Icd9flController(IIcd9flService Icd9flService)
        {
            _Icd9flService = Icd9flService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9flList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Icd9fls = await _Icd9flService.GetIcd9flListByValue(offset, limit, val);

            if (Icd9fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Icd9fls in database");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9fls);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9flList(string Icd9fl_name)
        {
            var Icd9fls = await _Icd9flService.GetIcd9flList(Icd9fl_name);

            if (Icd9fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd9fl found for uci: {Icd9fl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9fls);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9fl(string Icd9fl_name)
        {
            var Icd9fls = await _Icd9flService.GetIcd9fl(Icd9fl_name);

            if (Icd9fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd9fl found for uci: {Icd9fl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9fls);
        }

        [HttpPost]
        public async Task<ActionResult<Icd9fl>> AddIcd9fl(Icd9fl Icd9fl)
        {
            var dbIcd9fl = await _Icd9flService.AddIcd9fl(Icd9fl);

            if (dbIcd9fl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd9fl.TbIcd9flName} could not be added."
                );
            }

            return CreatedAtAction("GetIcd9fl", new { uci = Icd9fl.TbIcd9flName }, Icd9fl);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIcd9fl(Icd9fl Icd9fl)
        {           
            Icd9fl dbIcd9fl = await _Icd9flService.UpdateIcd9fl(Icd9fl);

            if (dbIcd9fl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd9fl.TbIcd9flName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIcd9fl(Icd9fl Icd9fl)
        {            
            (bool status, string message) = await _Icd9flService.DeleteIcd9fl(Icd9fl);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Icd9fl);
        }
    }
}
