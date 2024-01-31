using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsearchMvc.Controllers
{
    public class JobRequirementsController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: JobRequirements
        public async Task<IActionResult> Index()
        {
            var jobRequirementsList = await _context.JobRequirementsList.ToListAsync();
            return View(jobRequirementsList);
        }

        // GET: JobRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList
                .FirstOrDefaultAsync(m => m.JobRequirementId == id);

            if (jobRequirements == null)
            {
                return NotFound();
            }

            return View(jobRequirements);
        }

        // GET: JobRequirements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobRequirements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobRequirementId,JobRequirementText")] JobRequirements jobRequirements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobRequirements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobRequirements);
        }

        // GET: JobRequirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList.FindAsync(id);

            if (jobRequirements == null)
            {
                return NotFound();
            }

            return View(jobRequirements);
        }

        // POST: JobRequirements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobRequirementId,JobRequirementText")] JobRequirements jobRequirements)
        {
            if (id != jobRequirements.JobRequirementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobRequirements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobRequirementsExists(jobRequirements.JobRequirementId))
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
            return View(jobRequirements);
        }

        // GET: JobRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirements = await _context.JobRequirementsList
                .FirstOrDefaultAsync(m => m.JobRequirementId == id);

            if (jobRequirements == null)
            {
                return NotFound();
            }

            return View(jobRequirements);
        }

        // POST: JobRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRequirements = await _context.JobRequirementsList.FindAsync(id);
            _context.JobRequirementsList.Remove(jobRequirements);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobRequirementsExists(int id)
        {
            return _context.JobRequirementsList.Any(e => e.JobRequirementId == id);
        }
    }
}
