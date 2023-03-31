using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Icd9flPController : ControllerBase
    {
        private readonly IIcd9flPService _Icd9flPService;

        public Icd9flPController(IIcd9flPService Icd9flPService)
        {
            _Icd9flPService = Icd9flPService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9flPList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Icd9flPs = await _Icd9flPService.GetIcd9flPListByValue(offset, limit, val);

            if (Icd9flPs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Icd9flPs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9flPs);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9flPList(string Icd9flP_name)
        {
            var Icd9flPs = await _Icd9flPService.GetIcd9flPList(Icd9flP_name);

            if (Icd9flPs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd9flP found for uci: {Icd9flP_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9flPs);
        }

        [HttpGet]
        public async Task<IActionResult> GetIcd9flP(string Icd9flP_name)
        {
            var Icd9flPs = await _Icd9flPService.GetIcd9flP(Icd9flP_name);

            if (Icd9flPs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Icd9flP found for uci: {Icd9flP_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Icd9flPs);
        }

        [HttpPost]
        public async Task<ActionResult<Icd9flP>> AddIcd9flP(Icd9flP Icd9flP)
        {
            var dbIcd9flP = await _Icd9flPService.AddIcd9flP(Icd9flP);

            if (dbIcd9flP == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd9flP.TbIcd9flPName} could not be added."
                );
            }

            return CreatedAtAction("GetIcd9flP", new { uci = Icd9flP.TbIcd9flPName }, Icd9flP);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIcd9flP(Icd9flP Icd9flP)
        {           
            Icd9flP dbIcd9flP = await _Icd9flPService.UpdateIcd9flP(Icd9flP);

            if (dbIcd9flP == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Icd9flP.TbIcd9flPName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIcd9flP(Icd9flP Icd9flP)
        {            
            (bool status, string message) = await _Icd9flPService.DeleteIcd9flP(Icd9flP);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Icd9flP);
        }
    }
}
