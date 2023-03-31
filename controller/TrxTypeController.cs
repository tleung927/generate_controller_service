using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrxTypeController : ControllerBase
    {
        private readonly ITrxTypeService _TrxTypeService;

        public TrxTypeController(ITrxTypeService TrxTypeService)
        {
            _TrxTypeService = TrxTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TrxTypes = await _TrxTypeService.GetTrxTypeListByValue(offset, limit, val);

            if (TrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TrxTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, TrxTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxTypeList(string TrxType_name)
        {
            var TrxTypes = await _TrxTypeService.GetTrxTypeList(TrxType_name);

            if (TrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxType found for uci: {TrxType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrxType(string TrxType_name)
        {
            var TrxTypes = await _TrxTypeService.GetTrxType(TrxType_name);

            if (TrxTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TrxType found for uci: {TrxType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TrxTypes);
        }

        [HttpPost]
        public async Task<ActionResult<TrxType>> AddTrxType(TrxType TrxType)
        {
            var dbTrxType = await _TrxTypeService.AddTrxType(TrxType);

            if (dbTrxType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxType.TbTrxTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetTrxType", new { uci = TrxType.TbTrxTypeName }, TrxType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrxType(TrxType TrxType)
        {           
            TrxType dbTrxType = await _TrxTypeService.UpdateTrxType(TrxType);

            if (dbTrxType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TrxType.TbTrxTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrxType(TrxType TrxType)
        {            
            (bool status, string message) = await _TrxTypeService.DeleteTrxType(TrxType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TrxType);
        }
    }
}
