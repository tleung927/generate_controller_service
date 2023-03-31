using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperTraceController : ControllerBase
    {
        private readonly IOperTraceService _OperTraceService;

        public OperTraceController(IOperTraceService OperTraceService)
        {
            _OperTraceService = OperTraceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperTraceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var OperTraces = await _OperTraceService.GetOperTraceListByValue(offset, limit, val);

            if (OperTraces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No OperTraces in database");
            }

            return StatusCode(StatusCodes.Status200OK, OperTraces);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperTraceList(string OperTrace_name)
        {
            var OperTraces = await _OperTraceService.GetOperTraceList(OperTrace_name);

            if (OperTraces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperTrace found for uci: {OperTrace_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperTraces);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperTrace(string OperTrace_name)
        {
            var OperTraces = await _OperTraceService.GetOperTrace(OperTrace_name);

            if (OperTraces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperTrace found for uci: {OperTrace_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperTraces);
        }

        [HttpPost]
        public async Task<ActionResult<OperTrace>> AddOperTrace(OperTrace OperTrace)
        {
            var dbOperTrace = await _OperTraceService.AddOperTrace(OperTrace);

            if (dbOperTrace == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperTrace.TbOperTraceName} could not be added."
                );
            }

            return CreatedAtAction("GetOperTrace", new { uci = OperTrace.TbOperTraceName }, OperTrace);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperTrace(OperTrace OperTrace)
        {           
            OperTrace dbOperTrace = await _OperTraceService.UpdateOperTrace(OperTrace);

            if (dbOperTrace == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperTrace.TbOperTraceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOperTrace(OperTrace OperTrace)
        {            
            (bool status, string message) = await _OperTraceService.DeleteOperTrace(OperTrace);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, OperTrace);
        }
    }
}
