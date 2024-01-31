using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dal.Models;

namespace JobsearchMvc.Controllers
{
    public class JobNaturesController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public JobNaturesController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: JobNatures
        public async Task<IActionResult> Index()
        {
            try
            {
                var jobNatures = await _context.JobNatures.ToListAsync();
                return View(jobNatures);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: JobNatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobNatures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobNature jobNature)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.JobNatures.Add(jobNature);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            }

            return View(jobNature);
        }

        // GET: JobNatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNature = await _context.JobNatures.FindAsync(id);
            if (jobNature == null)
            {
                return NotFound();
            }

            return View(jobNature);
        }

        // POST: JobNatures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobNature jobNature)
        {
            if (id != jobNature.JobNatureId)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(jobNature).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!JobNatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(jobNature);
        }

        // GET: JobNatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNature = await _context.JobNatures
                .FirstOrDefaultAsync(m => m.JobNatureId == id);
            if (jobNature == null)
            {
                return NotFound();
            }

            return View(jobNature);
        }

        // POST: JobNatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobNature = await _context.JobNatures.FindAsync(id);
            if (jobNature == null)
            {
                return NotFound();
            }

            _context.JobNatures.Remove(jobNature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobNatureExists(int id)
        {
            return _context.JobNatures.Any(e => e.JobNatureId == id);
        }
    }
}
