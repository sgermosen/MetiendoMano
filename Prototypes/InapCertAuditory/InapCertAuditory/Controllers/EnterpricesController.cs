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
    public class EnterpricesController : Controller
    {
        private readonly DataContext _context;

        public EnterpricesController(DataContext context)
        {
            _context = context;
        }

        // GET: Enterprices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enterprice.ToListAsync());
        }

        // GET: Enterprices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterprice = await _context.Enterprice
                .FirstOrDefaultAsync(m => m.EnterpriceId == id);
            if (enterprice == null)
            {
                return NotFound();
            }

            return View(enterprice);
        }

        // GET: Enterprices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enterprices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnterpriceId,Name,Tel,Address,Observations")] Enterprice enterprice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enterprice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enterprice);
        }

        // GET: Enterprices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterprice = await _context.Enterprice.FindAsync(id);
            if (enterprice == null)
            {
                return NotFound();
            }
            return View(enterprice);
        }

        // POST: Enterprices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnterpriceId,Name,Tel,Address,Observations")] Enterprice enterprice)
        {
            if (id != enterprice.EnterpriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enterprice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnterpriceExists(enterprice.EnterpriceId))
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
            return View(enterprice);
        }

        // GET: Enterprices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterprice = await _context.Enterprice
                .FirstOrDefaultAsync(m => m.EnterpriceId == id);
            if (enterprice == null)
            {
                return NotFound();
            }

            return View(enterprice);
        }

        // POST: Enterprices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enterprice = await _context.Enterprice.FindAsync(id);
            _context.Enterprice.Remove(enterprice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnterpriceExists(int id)
        {
            return _context.Enterprice.Any(e => e.EnterpriceId == id);
        }
    }
}
