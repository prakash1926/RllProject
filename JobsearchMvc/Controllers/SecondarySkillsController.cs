using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsearchMvc.Controllers
{
    public class SecondarySkillsController : Controller
    {


        private readonly JobsPortalDbContext _context;

        public SecondarySkillsController(JobsPortalDbContext context)
        {
            _context = context;
        }

        // GET: SecondarySkills
        public IActionResult Index()
        {
            List<SecondarySkill> secondarySkills = _context.SecondarySkill.ToList();
            return View(secondarySkills);
        }

        // GET: SecondarySkills/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondarySkill = _context.SecondarySkill.FirstOrDefault(m => m.SecondarySkill_Id == id);
            if (secondarySkill == null)
            {
                return NotFound();
            }

            return View(secondarySkill);
        }

        // GET: SecondarySkills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecondarySkills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SecondarySkill_Id,SecondarySkill_Name")] SecondarySkill secondarySkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secondarySkill);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(secondarySkill);
        }

        // GET: SecondarySkills/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondarySkill = _context.SecondarySkill.FirstOrDefault(m => m.SecondarySkill_Id == id);
            if (secondarySkill == null)
            {
                return NotFound();
            }

            return View(secondarySkill);
        }

        // POST: SecondarySkills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SecondarySkill_Id,SecondarySkill_Name")] SecondarySkill secondarySkill)
        {
            if (id != secondarySkill.SecondarySkill_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondarySkill);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondarySkillExists(secondarySkill.SecondarySkill_Id))
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
            return View(secondarySkill);
        }

        // GET: SecondarySkills/Delete/5
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var secondarySkill = _context.SecondarySkill.FirstOrDefault(m => m.SecondarySkill_Id == id);
            if (secondarySkill == null)
            {
                return NotFound();
            }

            return View(secondarySkill);
        }

        // POST: SecondarySkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var secondarySkill = _context.SecondarySkill.Find(id);
            _context.SecondarySkill.Remove(secondarySkill);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondarySkillExists(int id)
        {
            return _context.SecondarySkill.Any(e => e.SecondarySkill_Id == id);
        }
    }
}