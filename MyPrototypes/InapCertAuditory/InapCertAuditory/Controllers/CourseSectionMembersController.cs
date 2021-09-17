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
    public class CourseSectionMembersController : Controller
    {
        private readonly DataContext _context;

        public CourseSectionMembersController(DataContext context)
        {
            _context = context;
        }

        // GET: CourseSectionMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseSectionMember.ToListAsync());
        }

        // GET: CourseSectionMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSectionMember = await _context.CourseSectionMember
                .FirstOrDefaultAsync(m => m.CourseSectionMemberId == id);
            if (courseSectionMember == null)
            {
                return NotFound();
            }

            return View(courseSectionMember);
        }

        // GET: CourseSectionMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseSectionMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseSectionMemberId,Observations,Punctuation")] CourseSectionMember courseSectionMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSectionMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseSectionMember);
        }

        // GET: CourseSectionMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSectionMember = await _context.CourseSectionMember.FindAsync(id);
            if (courseSectionMember == null)
            {
                return NotFound();
            }
            return View(courseSectionMember);
        }

        // POST: CourseSectionMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseSectionMemberId,Observations,Punctuation")] CourseSectionMember courseSectionMember)
        {
            if (id != courseSectionMember.CourseSectionMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSectionMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSectionMemberExists(courseSectionMember.CourseSectionMemberId))
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
            return View(courseSectionMember);
        }

        // GET: CourseSectionMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSectionMember = await _context.CourseSectionMember
                .FirstOrDefaultAsync(m => m.CourseSectionMemberId == id);
            if (courseSectionMember == null)
            {
                return NotFound();
            }

            return View(courseSectionMember);
        }

        // POST: CourseSectionMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSectionMember = await _context.CourseSectionMember.FindAsync(id);
            _context.CourseSectionMember.Remove(courseSectionMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSectionMemberExists(int id)
        {
            return _context.CourseSectionMember.Any(e => e.CourseSectionMemberId == id);
        }
    }
}
