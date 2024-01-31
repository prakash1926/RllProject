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
    public class PostJobsController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public PostJobsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostJob>>> GetPostJobs()
        {
            if (_context.PostJobs == null)
            {
                return NotFound();
            }

            return await _context.PostJobs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostJob>> GetPostJob(int id)
        {
            if (_context.PostJobs == null)
            {
                return NotFound();
            }

            var postJob = await _context.PostJobs.FindAsync(id);

            if (postJob == null)
            {
                return NotFound();
            }

            return postJob;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostJob(int id, PostJob postJob)
        {
            if (id != postJob.PostJobId)
            {
                return BadRequest();
            }

            var existingPostJob = await _context.PostJobs.FindAsync(id);

            if (existingPostJob == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingPostJob.JobTitle = postJob.JobTitle;
            existingPostJob.JobDescription = postJob.JobDescription;
            existingPostJob.MinSalary = postJob.MinSalary;
            existingPostJob.MaxSalary = postJob.MaxSalary;
            existingPostJob.Location = postJob.Location;
            existingPostJob.Vacancy = postJob.Vacancy;
            existingPostJob.ApplicationDeadline = postJob.ApplicationDeadline;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostJobExists(id))
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
        public async Task<ActionResult<PostJob>> PostPostJob(PostJob postJob)
        {
            if (_context.PostJobs == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.PostJobs' is null.");
            }

            _context.PostJobs.Add(postJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostJob", new { id = postJob.PostJobId }, postJob);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostJob(int id)
        {
            if (_context.PostJobs == null)
            {
                return NotFound();
            }

            var postJob = await _context.PostJobs.FindAsync(id);
            if (postJob == null)
            {
                return NotFound();
            }

            _context.PostJobs.Remove(postJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostJobExists(int id)
        {
            return (_context.PostJobs?.Any(e => e.PostJobId == id)).GetValueOrDefault();
        }
    }
}