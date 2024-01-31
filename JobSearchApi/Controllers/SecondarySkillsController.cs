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
    public class SecondarySkillsController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public SecondarySkillsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecondarySkill>>> GetSecondarySkill()
        {
            if (_context.SecondarySkill == null)
            {
                return NotFound();
            }

            return await _context.SecondarySkill.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SecondarySkill>> GetSecondarySkill(int id)
        {
            if (_context.SecondarySkill == null)
            {
                return NotFound();
            }

            var secondarySkill = await _context.SecondarySkill.FindAsync(id);

            if (secondarySkill == null)
            {
                return NotFound();
            }

            return secondarySkill;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecondarySkill(int id, SecondarySkill secondarySkill)
        {
            if (id != secondarySkill.SecondarySkill_Id)
            {
                return BadRequest();
            }

            var existingSecondarySkill = await _context.SecondarySkill.FindAsync(id);

            if (existingSecondarySkill == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingSecondarySkill.SecondarySkill_Name = secondarySkill.SecondarySkill_Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecondarySkillExists(id))
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
        public async Task<ActionResult<SecondarySkill>> PostSecondarySkill(SecondarySkill secondarySkill)
        {
            if (_context.SecondarySkill == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.SecondarySkill' is null.");
            }

            _context.SecondarySkill.Add(secondarySkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecondarySkill", new { id = secondarySkill.SecondarySkill_Id }, secondarySkill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecondarySkill(int id)
        {
            if (_context.SecondarySkill == null)
            {
                return NotFound();
            }

            var secondarySkill = await _context.SecondarySkill.FindAsync(id);
            if (secondarySkill == null)
            {
                return NotFound();
            }

            _context.SecondarySkill.Remove(secondarySkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecondarySkillExists(int id)
        {
            return (_context.SecondarySkill?.Any(e => e.SecondarySkill_Id == id)).GetValueOrDefault();
        }
    }
}