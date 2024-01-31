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
    public class UserTypesController : ControllerBase
    {
        private readonly JobsPortalDbContext _context;

        public UserTypesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }

            return await _context.UserTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetUserType(int id)
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }

            var userType = await _context.UserTypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(int id, UserType userType)
        {
            if (id != userType.UserTypeId)
            {
                return BadRequest();
            }

            var existingUserType = await _context.UserTypes.FindAsync(id);

            if (existingUserType == null)
            {
                return NotFound();
            }

            // Update properties based on your model
            existingUserType.UserTypeText = userType.UserTypeText;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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
        public async Task<ActionResult<UserType>> PostUserType(UserType userType)
        {
            if (_context.UserTypes == null)
            {
                return Problem("Entity set 'JobsPortalDbContext.UserTypes' is null.");
            }

            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.UserTypeId }, userType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }

            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTypeExists(int id)
        {
            return (_context.UserTypes?.Any(e => e.UserTypeId == id)).GetValueOrDefault();
        }
    }
}