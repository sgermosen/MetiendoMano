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
    public class TandasController : Controller
    {
        private readonly DataContext _context;

        public TandasController(DataContext context)
        {
            _context = context;
        }

        // GET: Tandas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tanda.ToListAsync());
        }

        // GET: Tandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanda = await _context.Tanda
                .FirstOrDefaultAsync(m => m.TandaId == id);
            if (tanda == null)
            {
                return NotFound();
            }

            return View(tanda);
        }

        // GET: Tandas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tandas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TandaId,Name")] Tanda tanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tanda);
        }

        // GET: Tandas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanda = await _context.Tanda.FindAsync(id);
            if (tanda == null)
            {
                return NotFound();
            }
            return View(tanda);
        }

        // POST: Tandas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TandaId,Name")] Tanda tanda)
        {
            if (id != tanda.TandaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TandaExists(tanda.TandaId))
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
            return View(tanda);
        }

        // GET: Tandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanda = await _context.Tanda
                .FirstOrDefaultAsync(m => m.TandaId == id);
            if (tanda == null)
            {
                return NotFound();
            }

            return View(tanda);
        }

        // POST: Tandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tanda = await _context.Tanda.FindAsync(id);
            _context.Tanda.Remove(tanda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TandaExists(int id)
        {
            return _context.Tanda.Any(e => e.TandaId == id);
        }
    }
}
