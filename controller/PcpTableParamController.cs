using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PcpTableParamController : ControllerBase
    {
        private readonly IPcpTableParamService _PcpTableParamService;

        public PcpTableParamController(IPcpTableParamService PcpTableParamService)
        {
            _PcpTableParamService = PcpTableParamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTableParamList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PcpTableParams = await _PcpTableParamService.GetPcpTableParamListByValue(offset, limit, val);

            if (PcpTableParams == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PcpTableParams in database");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTableParams);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTableParamList(string PcpTableParam_name)
        {
            var PcpTableParams = await _PcpTableParamService.GetPcpTableParamList(PcpTableParam_name);

            if (PcpTableParams == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpTableParam found for uci: {PcpTableParam_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTableParams);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTableParam(string PcpTableParam_name)
        {
            var PcpTableParams = await _PcpTableParamService.GetPcpTableParam(PcpTableParam_name);

            if (PcpTableParams == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpTableParam found for uci: {PcpTableParam_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTableParams);
        }

        [HttpPost]
        public async Task<ActionResult<PcpTableParam>> AddPcpTableParam(PcpTableParam PcpTableParam)
        {
            var dbPcpTableParam = await _PcpTableParamService.AddPcpTableParam(PcpTableParam);

            if (dbPcpTableParam == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpTableParam.TbPcpTableParamName} could not be added."
                );
            }

            return CreatedAtAction("GetPcpTableParam", new { uci = PcpTableParam.TbPcpTableParamName }, PcpTableParam);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePcpTableParam(PcpTableParam PcpTableParam)
        {           
            PcpTableParam dbPcpTableParam = await _PcpTableParamService.UpdatePcpTableParam(PcpTableParam);

            if (dbPcpTableParam == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpTableParam.TbPcpTableParamName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePcpTableParam(PcpTableParam PcpTableParam)
        {            
            (bool status, string message) = await _PcpTableParamService.DeletePcpTableParam(PcpTableParam);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PcpTableParam);
        }
    }
}
