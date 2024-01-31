using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsearchMvc.Controllers
{
    public class PrimarySkillController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public PrimarySkillController(JobsPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var primarySkills = await _context.PrimarySkill.ToListAsync();
            return View(primarySkills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primarySkill = await _context.PrimarySkill.FirstOrDefaultAsync(m => m.PrimarySkill_Id == id);
            if (primarySkill == null)
            {
                return NotFound();
            }

            return View(primarySkill);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimarySkill_Id,PrimarySkill_Name")] PrimarySkill primarySkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primarySkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(primarySkill);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primarySkill = await _context.PrimarySkill.FindAsync(id);
            if (primarySkill == null)
            {
                return NotFound();
            }
            return View(primarySkill);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrimarySkill_Id,PrimarySkill_Name")] PrimarySkill primarySkill)
        {
            if (id != primarySkill.PrimarySkill_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primarySkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimarySkillExists(primarySkill.PrimarySkill_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(primarySkill);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primarySkill = await _context.PrimarySkill.FirstOrDefaultAsync(m => m.PrimarySkill_Id == id);
            if (primarySkill == null)
            {
                return NotFound();
            }

            return View(primarySkill);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primarySkill = await _context.PrimarySkill.FindAsync(id);
            _context.PrimarySkill.Remove(primarySkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimarySkillExists(int id)
        {
            return _context.PrimarySkill.Any(e => e.PrimarySkill_Id == id);
        }
    }
}
