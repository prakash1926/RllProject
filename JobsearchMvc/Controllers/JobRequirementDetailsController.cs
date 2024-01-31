using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsearchMvc.Controllers
{
    public class JobRequirementDetailsController : Controller
    {
        private readonly JobsPortalDbContext _context;

        public JobRequirementDetailsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: JobRequirementDetails
        public async Task<IActionResult> Index()
        {
            try
            {
                var jobRequirementDetailsList = await _context.JobRequirementDetailsList.ToListAsync();
                return View(jobRequirementDetailsList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: JobRequirementDetails/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                return View(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: JobRequirementDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobRequirementDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobRequirementDetails jobRequirementDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.JobRequirementDetailsList.Add(jobRequirementDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: JobRequirementDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                return View(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST: JobRequirementDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobRequirementDetails jobRequirementDetails)
        {
            try
            {
                if (id != jobRequirementDetails.JobRequirementDetailsId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _context.Entry(jobRequirementDetails).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET: JobRequirementDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                return View(jobRequirementDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST: JobRequirementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var jobRequirementDetails = await _context.JobRequirementDetailsList.FindAsync(id);

                if (jobRequirementDetails == null)
                {
                    return NotFound();
                }

                _context.JobRequirementDetailsList.Remove(jobRequirementDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }

}
