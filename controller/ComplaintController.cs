using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _ComplaintService;

        public ComplaintController(IComplaintService ComplaintService)
        {
            _ComplaintService = ComplaintService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComplaintList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Complaints = await _ComplaintService.GetComplaintListByValue(offset, limit, val);

            if (Complaints == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Complaints in database");
            }

            return StatusCode(StatusCodes.Status200OK, Complaints);
        }

        [HttpGet]
        public async Task<IActionResult> GetComplaintList(string Complaint_name)
        {
            var Complaints = await _ComplaintService.GetComplaintList(Complaint_name);

            if (Complaints == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Complaint found for uci: {Complaint_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Complaints);
        }

        [HttpGet]
        public async Task<IActionResult> GetComplaint(string Complaint_name)
        {
            var Complaints = await _ComplaintService.GetComplaint(Complaint_name);

            if (Complaints == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Complaint found for uci: {Complaint_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Complaints);
        }

        [HttpPost]
        public async Task<ActionResult<Complaint>> AddComplaint(Complaint Complaint)
        {
            var dbComplaint = await _ComplaintService.AddComplaint(Complaint);

            if (dbComplaint == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Complaint.TbComplaintName} could not be added."
                );
            }

            return CreatedAtAction("GetComplaint", new { uci = Complaint.TbComplaintName }, Complaint);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComplaint(Complaint Complaint)
        {           
            Complaint dbComplaint = await _ComplaintService.UpdateComplaint(Complaint);

            if (dbComplaint == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Complaint.TbComplaintName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComplaint(Complaint Complaint)
        {            
            (bool status, string message) = await _ComplaintService.DeleteComplaint(Complaint);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Complaint);
        }
    }
}
