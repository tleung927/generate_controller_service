using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferReqController : ControllerBase
    {
        private readonly ITransferReqService _TransferReqService;

        public TransferReqController(ITransferReqService TransferReqService)
        {
            _TransferReqService = TransferReqService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferReqList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TransferReqs = await _TransferReqService.GetTransferReqListByValue(offset, limit, val);

            if (TransferReqs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TransferReqs in database");
            }

            return StatusCode(StatusCodes.Status200OK, TransferReqs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferReqList(string TransferReq_name)
        {
            var TransferReqs = await _TransferReqService.GetTransferReqList(TransferReq_name);

            if (TransferReqs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransferReq found for uci: {TransferReq_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransferReqs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransferReq(string TransferReq_name)
        {
            var TransferReqs = await _TransferReqService.GetTransferReq(TransferReq_name);

            if (TransferReqs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TransferReq found for uci: {TransferReq_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TransferReqs);
        }

        [HttpPost]
        public async Task<ActionResult<TransferReq>> AddTransferReq(TransferReq TransferReq)
        {
            var dbTransferReq = await _TransferReqService.AddTransferReq(TransferReq);

            if (dbTransferReq == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransferReq.TbTransferReqName} could not be added."
                );
            }

            return CreatedAtAction("GetTransferReq", new { uci = TransferReq.TbTransferReqName }, TransferReq);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransferReq(TransferReq TransferReq)
        {           
            TransferReq dbTransferReq = await _TransferReqService.UpdateTransferReq(TransferReq);

            if (dbTransferReq == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TransferReq.TbTransferReqName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransferReq(TransferReq TransferReq)
        {            
            (bool status, string message) = await _TransferReqService.DeleteTransferReq(TransferReq);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TransferReq);
        }
    }
}
