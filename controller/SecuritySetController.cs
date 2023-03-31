using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecuritySetController : ControllerBase
    {
        private readonly ISecuritySetService _SecuritySetService;

        public SecuritySetController(ISecuritySetService SecuritySetService)
        {
            _SecuritySetService = SecuritySetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSecuritySetList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SecuritySets = await _SecuritySetService.GetSecuritySetListByValue(offset, limit, val);

            if (SecuritySets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SecuritySets in database");
            }

            return StatusCode(StatusCodes.Status200OK, SecuritySets);
        }

        [HttpGet]
        public async Task<IActionResult> GetSecuritySetList(string SecuritySet_name)
        {
            var SecuritySets = await _SecuritySetService.GetSecuritySetList(SecuritySet_name);

            if (SecuritySets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SecuritySet found for uci: {SecuritySet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SecuritySets);
        }

        [HttpGet]
        public async Task<IActionResult> GetSecuritySet(string SecuritySet_name)
        {
            var SecuritySets = await _SecuritySetService.GetSecuritySet(SecuritySet_name);

            if (SecuritySets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SecuritySet found for uci: {SecuritySet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SecuritySets);
        }

        [HttpPost]
        public async Task<ActionResult<SecuritySet>> AddSecuritySet(SecuritySet SecuritySet)
        {
            var dbSecuritySet = await _SecuritySetService.AddSecuritySet(SecuritySet);

            if (dbSecuritySet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SecuritySet.TbSecuritySetName} could not be added."
                );
            }

            return CreatedAtAction("GetSecuritySet", new { uci = SecuritySet.TbSecuritySetName }, SecuritySet);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSecuritySet(SecuritySet SecuritySet)
        {           
            SecuritySet dbSecuritySet = await _SecuritySetService.UpdateSecuritySet(SecuritySet);

            if (dbSecuritySet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SecuritySet.TbSecuritySetName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSecuritySet(SecuritySet SecuritySet)
        {            
            (bool status, string message) = await _SecuritySetService.DeleteSecuritySet(SecuritySet);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SecuritySet);
        }
    }
}
