using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ButtonLabelController : ControllerBase
    {
        private readonly IButtonLabelService _ButtonLabelService;

        public ButtonLabelController(IButtonLabelService ButtonLabelService)
        {
            _ButtonLabelService = ButtonLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetButtonLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ButtonLabels = await _ButtonLabelService.GetButtonLabelListByValue(offset, limit, val);

            if (ButtonLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ButtonLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, ButtonLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetButtonLabelList(string ButtonLabel_name)
        {
            var ButtonLabels = await _ButtonLabelService.GetButtonLabelList(ButtonLabel_name);

            if (ButtonLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ButtonLabel found for uci: {ButtonLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ButtonLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetButtonLabel(string ButtonLabel_name)
        {
            var ButtonLabels = await _ButtonLabelService.GetButtonLabel(ButtonLabel_name);

            if (ButtonLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ButtonLabel found for uci: {ButtonLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ButtonLabels);
        }

        [HttpPost]
        public async Task<ActionResult<ButtonLabel>> AddButtonLabel(ButtonLabel ButtonLabel)
        {
            var dbButtonLabel = await _ButtonLabelService.AddButtonLabel(ButtonLabel);

            if (dbButtonLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ButtonLabel.TbButtonLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetButtonLabel", new { uci = ButtonLabel.TbButtonLabelName }, ButtonLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateButtonLabel(ButtonLabel ButtonLabel)
        {           
            ButtonLabel dbButtonLabel = await _ButtonLabelService.UpdateButtonLabel(ButtonLabel);

            if (dbButtonLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ButtonLabel.TbButtonLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteButtonLabel(ButtonLabel ButtonLabel)
        {            
            (bool status, string message) = await _ButtonLabelService.DeleteButtonLabel(ButtonLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ButtonLabel);
        }
    }
}
