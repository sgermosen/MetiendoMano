using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InapCertAuditory.Models;

namespace InapCertAuditory.Controllers
{
    public class CourseSectionsController : Controller
    {
        private readonly DataContext _context;

        public CourseSectionsController(DataContext context)
        {
            _context = context;
        }

        // GET: CourseSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseSection.ToListAsync());
        }

        // GET: CourseSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSection
                .FirstOrDefaultAsync(m => m.CourseSectionId == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // GET: CourseSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseSectionId,Code,DateFrom,DateTo,Observations")] CourseSection courseSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseSection);
        }

        // GET: CourseSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSection.FindAsync(id);
            if (courseSection == null)
            {
                return NotFound();
            }
            return View(courseSection);
        }

        // POST: CourseSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseSectionId,Code,DateFrom,DateTo,Observations")] CourseSection courseSection)
        {
            if (id != courseSection.CourseSectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSectionExists(courseSection.CourseSectionId))
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
            return View(courseSection);
        }

        // GET: CourseSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSection
                .FirstOrDefaultAsync(m => m.CourseSectionId == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // POST: CourseSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSection = await _context.CourseSection.FindAsync(id);
            _context.CourseSection.Remove(courseSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSectionExists(int id)
        {
            return _context.CourseSection.Any(e => e.CourseSectionId == id);
        }
    }
}
