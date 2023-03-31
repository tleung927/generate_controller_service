using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferTempController : ControllerBase
    {
        private readonly ITransferTempService _TransferTempService;

        public TransferTempController(ITransferTempService TransferTempService)
        {
            _TransferTempService = TransferTempService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferTempList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransferTemps = await _TransferTempService.GetTransferTempListByValue(offset, limit, val);

            if (TransferTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransferTemps in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransferTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferTempList(string TransferTemp_name)
        {
            var TransferTemps = await _TransferTempService.GetTransferTempList(TransferTemp_name);

            if (TransferTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransferTemp found for uci: {TransferTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransferTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferTemp(string TransferTemp_name)
        {
            var TransferTemps = await _TransferTempService.GetTransferTemp(TransferTemp_name);

            if (TransferTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransferTemp found for uci: {TransferTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransferTemps);
        }

        [HttpPost]
        public async Task<ActionResult<TransferTemp>> AddTransferTemp(TransferTemp TransferTemp)
        {
            var dbTransferTemp = await _TransferTempService.AddTransferTemp(TransferTemp);

            if (dbTransferTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransferTemp.TbTransferTempName} could not be added."
                );
            }

            return CreatedAtAction("GetTransferTemp", new { uci = TransferTemp.TbTransferTempName }, TransferTemp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransferTemp(TransferTemp TransferTemp)
        {           
            TransferTemp dbTransferTemp = await _TransferTempService.UpdateTransferTemp(TransferTemp);

            if (dbTransferTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransferTemp.TbTransferTempName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransferTemp(TransferTemp TransferTemp)
        {            
            (bool status, string message) = await _TransferTempService.DeleteTransferTemp(TransferTemp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransferTemp);
        }
    }
}
