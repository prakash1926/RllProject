using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchMvc.Controllers
{
    public class PostJobsController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public PostJobsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: PostJobs
        public async Task<IActionResult> Index()
        {
            List<PostJob> postJobs = await _context.PostJobs.ToListAsync();
            return View(postJobs);
        }

        // GET: PostJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postJob = await _context.PostJobs.FirstOrDefaultAsync(m => m.PostJobId == id);
            if (postJob == null)
            {
                return NotFound();
            }

            return View(postJob);
        }

        // GET: PostJobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostJobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostJobId,UserId,CompanyId,JobCategoryId,JobTitle,JobDescription,MinSalary,MaxSalary,Location,Vacancy,JobNatureId,PostDate,ApplicationDeadline,LastDate,JobStatusId")] PostJob postJob)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(postJob);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                ModelState.AddModelError("", "An error occurred while saving the PostJob.");
            }
            return View(postJob);
        }

        // GET: PostJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postJob = await _context.PostJobs.FindAsync(id);
            if (postJob == null)
            {
                return NotFound();
            }
            return View(postJob);
        }

        // POST: PostJobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostJobId,UserId,CompanyId,JobCategoryId,JobTitle,JobDescription,MinSalary,MaxSalary,Location,Vacancy,JobNatureId,PostDate,ApplicationDeadline,LastDate,JobStatusId")] PostJob postJob)
        {
            if (id != postJob.PostJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostJobExists(postJob.PostJobId))
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
            return View(postJob);
        }

        // GET: PostJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postJob = await _context.PostJobs.FirstOrDefaultAsync(m => m.PostJobId == id);
            if (postJob == null)
            {
                return NotFound();
            }

            return View(postJob);
        }

        // POST: PostJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postJob = await _context.PostJobs.FindAsync(id);
            _context.PostJobs.Remove(postJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostJobExists(int id)
        {
            return _context.PostJobs.Any(e => e.PostJobId == id);
        }
    }
}