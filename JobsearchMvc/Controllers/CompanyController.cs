using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dal.Models;
using Microsoft.AspNetCore.Hosting;

namespace JobsearchMvc.Controllers
{
    public class CompanyController : Controller
    {
        private readonly JobsPortalDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(JobsPortalDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company companyViewModel)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (companyViewModel.Logo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + companyViewModel.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        companyViewModel.Logo.CopyTo(fileStream);
                    }

                    // Save the file path to the database
                    companyViewModel.LogoPath = "/uploads/" + uniqueFileName;
                }

                // Map the view model to the entity
                Company company = new Company
                {
                    UserId = companyViewModel.UserId,
                    CompanyName = companyViewModel.CompanyName,
                    ContactNo = companyViewModel.ContactNo,
                    PhoneNo = companyViewModel.PhoneNo,
                    EmailAddress = companyViewModel.EmailAddress,
                    Description = companyViewModel.Description,
                    LogoPath = companyViewModel.LogoPath
                };

                // Save the entity to the database
                _context.Companies.Add(company);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(companyViewModel);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(company).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
