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
    public class JobCategoriesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public JobCategoriesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/JobCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategory>>> GetJobCategories()
        {
            try
            {
                var jobCategories = await _context.JobCategories.ToListAsync();

                if (jobCategories == null || !jobCategories.Any())
                {
                    return NotFound();
                }

                return Ok(jobCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: api/JobCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategory>> GetJobCategory(int id)
        {
            try
            {
                var jobCategory = await _context.JobCategories.FindAsync(id);

                if (jobCategory == null)
                {
                    return NotFound();
                }

                return Ok(jobCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // PUT: api/JobCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCategory(int id, JobCategory jobCategory)
        {
            try
            {
                if (id != jobCategory.JobCategoryId)
                {
                    return BadRequest();
                }

                var existingJobCategory = await _context.JobCategories.FindAsync(id);

                if (existingJobCategory == null)
                {
                    return NotFound();
                }

                existingJobCategory.JobCategoryName = jobCategory.JobCategoryName;
                // Update other properties as needed

                _context.Entry(existingJobCategory).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST: api/JobCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobCategory>> PostJobCategory(JobCategory jobCategory)
        {
            try
            {
                if (jobCategory == null)
                {
                    return BadRequest();
                }

                _context.JobCategories.Add(jobCategory);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetJobCategory", new { id = jobCategory.JobCategoryId }, jobCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // DELETE: api/JobCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCategory(int id)
        {
            try
            {
                var jobCategory = await _context.JobCategories.FindAsync(id);

                if (jobCategory == null)
                {
                    return NotFound();
                }

                _context.JobCategories.Remove(jobCategory);
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