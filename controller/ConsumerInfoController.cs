using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerInfoController : ControllerBase
    {
        private readonly IConsumerInfoService _ConsumerInfoService;

        public ConsumerInfoController(IConsumerInfoService ConsumerInfoService)
        {
            _ConsumerInfoService = ConsumerInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerInfoList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerInfos = await _ConsumerInfoService.GetConsumerInfoListByValue(offset, limit, val);

            if (ConsumerInfos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerInfos in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerInfos);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerInfoList(string ConsumerInfo_name)
        {
            var ConsumerInfos = await _ConsumerInfoService.GetConsumerInfoList(ConsumerInfo_name);

            if (ConsumerInfos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerInfo found for uci: {ConsumerInfo_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerInfos);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerInfo(string ConsumerInfo_name)
        {
            var ConsumerInfos = await _ConsumerInfoService.GetConsumerInfo(ConsumerInfo_name);

            if (ConsumerInfos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerInfo found for uci: {ConsumerInfo_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerInfos);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerInfo>> AddConsumerInfo(ConsumerInfo ConsumerInfo)
        {
            var dbConsumerInfo = await _ConsumerInfoService.AddConsumerInfo(ConsumerInfo);

            if (dbConsumerInfo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerInfo.TbConsumerInfoName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerInfo", new { uci = ConsumerInfo.TbConsumerInfoName }, ConsumerInfo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerInfo(ConsumerInfo ConsumerInfo)
        {           
            ConsumerInfo dbConsumerInfo = await _ConsumerInfoService.UpdateConsumerInfo(ConsumerInfo);

            if (dbConsumerInfo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerInfo.TbConsumerInfoName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerInfo(ConsumerInfo ConsumerInfo)
        {            
            (bool status, string message) = await _ConsumerInfoService.DeleteConsumerInfo(ConsumerInfo);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerInfo);
        }
    }
}
