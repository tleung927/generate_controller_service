using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Icd10flController : ControllerBase
    {
        private readonly IIcd10flService _Icd10flService;

        public Icd10flController(IIcd10flService Icd10flService)
        {
            _Icd10flService = Icd10flService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd10flList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Icd10fls = await _Icd10flService.GetIcd10flListByValue(offset, limit, val);

            if (Icd10fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Icd10fls in database");
            }

            return StatusCode(StatusCodes.Status200OK, Icd10fls);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd10flList(string Icd10fl_name)
        {
            var Icd10fls = await _Icd10flService.GetIcd10flList(Icd10fl_name);

            if (Icd10fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd10fl found for uci: {Icd10fl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd10fls);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd10fl(string Icd10fl_name)
        {
            var Icd10fls = await _Icd10flService.GetIcd10fl(Icd10fl_name);

            if (Icd10fls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd10fl found for uci: {Icd10fl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd10fls);
        }

        [HttpPost]
        public async Task<ActionResult<Icd10fl>> AddIcd10fl(Icd10fl Icd10fl)
        {
            var dbIcd10fl = await _Icd10flService.AddIcd10fl(Icd10fl);

            if (dbIcd10fl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd10fl.TbIcd10flName} could not be added."
                );
            }

            return CreatedAtAction("GetIcd10fl", new { uci = Icd10fl.TbIcd10flName }, Icd10fl);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIcd10fl(Icd10fl Icd10fl)
        {           
            Icd10fl dbIcd10fl = await _Icd10flService.UpdateIcd10fl(Icd10fl);

            if (dbIcd10fl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd10fl.TbIcd10flName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIcd10fl(Icd10fl Icd10fl)
        {            
            (bool status, string message) = await _Icd10flService.DeleteIcd10fl(Icd10fl);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Icd10fl);
        }
    }
}
