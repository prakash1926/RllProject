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
    public class PrimarySkillsController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public PrimarySkillsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrimarySkill>>> GetPrimarySkill()
        {
            if (_context.PrimarySkill == null)
            {
                return NotFound();
            }

            return await _context.PrimarySkill.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrimarySkill>> GetPrimarySkill(int id)
        {
            if (_context.PrimarySkill == null)
            {
                return NotFound();
            }

            var primarySkill = await _context.PrimarySkill.FindAsync(id);

            if (primarySkill == null)
            {
                return NotFound();
            }

            return primarySkill;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrimarySkill(int id, PrimarySkill primarySkill)
        {
            if (id != primarySkill.PrimarySkill_Id)
            {
                return BadRequest();
            }

            var existingPrimarySkill = await _context.PrimarySkill.FindAsync(id);

            if (existingPrimarySkill == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingPrimarySkill.PrimarySkill_Name = primarySkill.PrimarySkill_Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrimarySkillExists(id))
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
        public async Task<ActionResult<PrimarySkill>> PostPrimarySkill(PrimarySkill primarySkill)
        {
            if (_context.PrimarySkill == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.PrimarySkill' is null.");
            }

            _context.PrimarySkill.Add(primarySkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrimarySkill", new { id = primarySkill.PrimarySkill_Id }, primarySkill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrimarySkill(int id)
        {
            if (_context.PrimarySkill == null)
            {
                return NotFound();
            }

            var primarySkill = await _context.PrimarySkill.FindAsync(id);
            if (primarySkill == null)
            {
                return NotFound();
            }

            _context.PrimarySkill.Remove(primarySkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrimarySkillExists(int id)
        {
            return (_context.PrimarySkill?.Any(e => e.PrimarySkill_Id == id)).GetValueOrDefault();
        }
    }
}