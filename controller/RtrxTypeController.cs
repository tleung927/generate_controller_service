using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RtrxTypeController : ControllerBase
    {
        private readonly IRtrxTypeService _RtrxTypeService;

        public RtrxTypeController(IRtrxTypeService RtrxTypeService)
        {
            _RtrxTypeService = RtrxTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRtrxTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var RtrxTypes = await _RtrxTypeService.GetRtrxTypeListByValue(offset, limit, val);

            if (RtrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No RtrxTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, RtrxTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtrxTypeList(string RtrxType_name)
        {
            var RtrxTypes = await _RtrxTypeService.GetRtrxTypeList(RtrxType_name);

            if (RtrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RtrxType found for uci: {RtrxType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RtrxTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtrxType(string RtrxType_name)
        {
            var RtrxTypes = await _RtrxTypeService.GetRtrxType(RtrxType_name);

            if (RtrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No RtrxType found for uci: {RtrxType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, RtrxTypes);
        }

        [HttpPost]
        public async Task<ActionResult<RtrxType>> AddRtrxType(RtrxType RtrxType)
        {
            var dbRtrxType = await _RtrxTypeService.AddRtrxType(RtrxType);

            if (dbRtrxType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RtrxType.TbRtrxTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetRtrxType", new { uci = RtrxType.TbRtrxTypeName }, RtrxType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRtrxType(RtrxType RtrxType)
        {           
            RtrxType dbRtrxType = await _RtrxTypeService.UpdateRtrxType(RtrxType);

            if (dbRtrxType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{RtrxType.TbRtrxTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRtrxType(RtrxType RtrxType)
        {            
            (bool status, string message) = await _RtrxTypeService.DeleteRtrxType(RtrxType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, RtrxType);
        }
    }
}
