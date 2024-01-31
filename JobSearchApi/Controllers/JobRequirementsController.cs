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
    public class JobRequirementsController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRequirements>>> GetJobRequirementsList()
        {
            if (_context.JobRequirementsList == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList.ToListAsync();
            return Ok(jobRequirements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobRequirements>> GetJobRequirements(int id)
        {
            if (_context.JobRequirementsList == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList.FindAsync(id);

            if (jobRequirements == null)
            {
                return NotFound();
            }

            return Ok(jobRequirements);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRequirements(int id, JobRequirements jobRequirements)
        {
            if (id != jobRequirements.JobRequirementId)
            {
                return BadRequest();
            }

            var existingJobRequirements = await _context.JobRequirementsList.FindAsync(id);

            if (existingJobRequirements == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingJobRequirements.JobRequirementText = jobRequirements.JobRequirementText;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRequirementsExists(id))
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
        public async Task<ActionResult<JobRequirements>> PostJobRequirements(JobRequirements jobRequirements)
        {
            if (_context.JobRequirementsList == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.JobRequirementsList' is null.");
            }

            _context.JobRequirementsList.Add(jobRequirements);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobRequirements", new { id = jobRequirements.JobRequirementId }, jobRequirements);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobRequirements(int id)
        {
            if (_context.JobRequirementsList == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList.FindAsync(id);
            if (jobRequirements == null)
            {
                return NotFound();
            }

            _context.JobRequirementsList.Remove(jobRequirements);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobRequirementsExists(int id)
        {
            return (_context.JobRequirementsList?.Any(e => e.JobRequirementId == id)).GetValueOrDefault();
        }
    }
}