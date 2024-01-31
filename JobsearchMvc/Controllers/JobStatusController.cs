using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchMvc.Controllers
{
    public class JobStatusController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public JobStatusController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: JobStatus
        public async Task<IActionResult> Index()
        {
            List<JobStatus> jobStatuses = await _context.JobStatuses.ToListAsync();
            return View(jobStatuses);
        }

        // GET: JobStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStatus = await _context.JobStatuses.FirstOrDefaultAsync(m => m.JobStatusId == id);
            if (jobStatus == null)
            {
                return NotFound();
            }

            return View(jobStatus);
        }

        // GET: JobStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobStatusId,JobStatusName,StatusMessage")] JobStatus jobStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobStatus);
        }

        // GET: JobStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStatus = await _context.JobStatuses.FindAsync(id);
            if (jobStatus == null)
            {
                return NotFound();
            }
            return View(jobStatus);
        }

        // POST: JobStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobStatusId,JobStatusName,StatusMessage")] JobStatus jobStatus)
        {
            if (id != jobStatus.JobStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobStatusExists(jobStatus.JobStatusId))
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
            return View(jobStatus);
        }

        // GET: JobStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStatus = await _context.JobStatuses.FirstOrDefaultAsync(m => m.JobStatusId == id);
            if (jobStatus == null)
            {
                return NotFound();
            }

            return View(jobStatus);
        }

        // POST: JobStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobStatus = await _context.JobStatuses.FindAsync(id);
            _context.JobStatuses.Remove(jobStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobStatusExists(int id)
        {
            return _context.JobStatuses.Any(e => e.JobStatusId == id);
        }
    }
}