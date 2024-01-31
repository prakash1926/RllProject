using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dal.Models;

namespace JobSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequirementDetailsController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementDetailsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRequirementDetails>>> GetJobRequirementDetailsList()
        {
            try
            {
                var jobRequirementDetailsList = await _context.JobRequirementDetailsList.ToListAsync();

                if (jobRequirementDetailsList == null || jobRequirementDetailsList.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jobRequirementDetailsList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobRequirementDetails>> GetJobRequirementDetails(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                return Ok(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRequirementDetails(int id, JobRequirementDetails jobRequirementDetails)
        {
            try
            {
                if (id != jobRequirementDetails.JobRequirementDetailsId)
                {
                    return BadRequest();
                }

                var existingJobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (existingJobRequirementDetails == null)
                {
                    return NotFound();
                }

                // Update properties as needed
                existingJobRequirementDetails.JobRequirementDetailsText = jobRequirementDetails.JobRequirementDetailsText;
                existingJobRequirementDetails.JobRequirementId = jobRequirementDetails.JobRequirementId;
                existingJobRequirementDetails.PostJobId = jobRequirementDetails.PostJobId;

                _context.Entry(existingJobRequirementDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobRequirementDetails>> PostJobRequirementDetails(JobRequirementDetails jobRequirementDetails)
        {
            try
            {
                if (jobRequirementDetails == null)
                {
                    return BadRequest();
                }

                _context.JobRequirementDetailsList.Add(jobRequirementDetails);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetJobRequirementDetails", new { id = jobRequirementDetails.JobRequirementDetailsId }, jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobRequirementDetails(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                _context.JobRequirementDetailsList.Remove(jobRequirementDetails);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}