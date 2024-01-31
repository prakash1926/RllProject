using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dal.Models;

namespace JobSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobNaturesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobNaturesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobNature>>> GetJobNatures()
        {
            try
            {
                var jobNatures = await _context.JobNatures.ToListAsync();

                if (jobNatures == null || jobNatures.Count == 0)
                {
                    return NotFound();
                }

                return Ok(jobNatures);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobNature>> GetJobNature(int id)
        {
            try
            {
                var jobNature = await _context.JobNatures.FindAsync(id);

                if (jobNature == null)
                {
                    return NotFound();
                }

                return Ok(jobNature);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobNature(int id, JobNature jobNature)
        {
            try
            {
                if (id != jobNature.JobNatureId)
                {
                    return BadRequest();
                }

                var existingJobNature = await _context.JobNatures.FindAsync(id);

                if (existingJobNature == null)
                {
                    return NotFound();
                }

                existingJobNature.JobNatureName = jobNature.JobNatureName;
                // Update other properties as needed

                _context.Entry(existingJobNature).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobNature>> PostJobNature(JobNature jobNature)
        {
            try
            {
                if (jobNature == null)
                {
                    return BadRequest();
                }

                _context.JobNatures.Add(jobNature);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetJobNature", new { id = jobNature.JobNatureId }, jobNature);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobNature(int id)
        {
            try
            {
                var jobNature = await _context.JobNatures.FindAsync(id);

                if (jobNature == null)
                {
                    return NotFound();
                }

                _context.JobNatures.Remove(jobNature);
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