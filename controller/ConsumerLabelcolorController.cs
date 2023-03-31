using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLabelcolorController : ControllerBase
    {
        private readonly IConsumerLabelcolorService _ConsumerLabelcolorService;

        public ConsumerLabelcolorController(IConsumerLabelcolorService ConsumerLabelcolorService)
        {
            _ConsumerLabelcolorService = ConsumerLabelcolorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabelcolorList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLabelcolors = await _ConsumerLabelcolorService.GetConsumerLabelcolorListByValue(offset, limit, val);

            if (ConsumerLabelcolors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLabelcolors in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabelcolors);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabelcolorList(string ConsumerLabelcolor_name)
        {
            var ConsumerLabelcolors = await _ConsumerLabelcolorService.GetConsumerLabelcolorList(ConsumerLabelcolor_name);

            if (ConsumerLabelcolors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabelcolor found for uci: {ConsumerLabelcolor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabelcolors);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabelcolor(string ConsumerLabelcolor_name)
        {
            var ConsumerLabelcolors = await _ConsumerLabelcolorService.GetConsumerLabelcolor(ConsumerLabelcolor_name);

            if (ConsumerLabelcolors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabelcolor found for uci: {ConsumerLabelcolor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabelcolors);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLabelcolor>> AddConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {
            var dbConsumerLabelcolor = await _ConsumerLabelcolorService.AddConsumerLabelcolor(ConsumerLabelcolor);

            if (dbConsumerLabelcolor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabelcolor.TbConsumerLabelcolorName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLabelcolor", new { uci = ConsumerLabelcolor.TbConsumerLabelcolorName }, ConsumerLabelcolor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {           
            ConsumerLabelcolor dbConsumerLabelcolor = await _ConsumerLabelcolorService.UpdateConsumerLabelcolor(ConsumerLabelcolor);

            if (dbConsumerLabelcolor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabelcolor.TbConsumerLabelcolorName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {            
            (bool status, string message) = await _ConsumerLabelcolorService.DeleteConsumerLabelcolor(ConsumerLabelcolor);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabelcolor);
        }
    }
}
