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
    public class JobStatusController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobStatusController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobStatus>>> GetJobStatuses()
        {
            if (_context.JobStatuses == null)
            {
                return NotFound();
            }

            return await _context.JobStatuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobStatus>> GetJobStatus(int id)
        {
            if (_context.JobStatuses == null)
            {
                return NotFound();
            }

            var jobStatus = await _context.JobStatuses.FindAsync(id);

            if (jobStatus == null)
            {
                return NotFound();
            }

            return jobStatus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobStatus(int id, JobStatus jobStatus)
        {
            if (id != jobStatus.JobStatusId)
            {
                return BadRequest();
            }

            var existingJobStatus = await _context.JobStatuses.FindAsync(id);

            if (existingJobStatus == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingJobStatus.JobStatusName = jobStatus.JobStatusName;
            existingJobStatus.StatusMessage = jobStatus.StatusMessage;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<JobStatus>> PostJobStatus(JobStatus jobStatus)
        {
            if (_context.JobStatuses == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.JobStatuses' is null.");
            }

            _context.JobStatuses.Add(jobStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobStatus", new { id = jobStatus.JobStatusId }, jobStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobStatus(int id)
        {
            if (_context.JobStatuses == null)
            {
                return NotFound();
            }

            var jobStatus = await _context.JobStatuses.FindAsync(id);
            if (jobStatus == null)
            {
                return NotFound();
            }

            _context.JobStatuses.Remove(jobStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobStatusExists(int id)
        {
            return (_context.JobStatuses?.Any(e => e.JobStatusId == id)).GetValueOrDefault();
        }
    }
}