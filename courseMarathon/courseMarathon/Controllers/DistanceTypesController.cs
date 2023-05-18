using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using courseMarathon.Models;

namespace courseMarathon.Controllers
{
    public class DistanceTypesController : Controller
    {
        private readonly MarathonContext _context;

        public DistanceTypesController(MarathonContext context)
        {
            _context = context;
        }

        // GET: DistanceTypes
        public async Task<IActionResult> Index()
        {
              return _context.DistanceTypes != null ? 
                          View(await _context.DistanceTypes.ToListAsync()) :
                          Problem("Entity set 'MarathonContext.DistanceTypes'  is null.");
        }

        // GET: DistanceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DistanceTypes == null)
            {
                return NotFound();
            }

            var distanceType = await _context.DistanceTypes
                .FirstOrDefaultAsync(m => m.DistanceTypeId == id);
            if (distanceType == null)
            {
                return NotFound();
            }

            return View(distanceType);
        }

        // GET: DistanceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DistanceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistanceTypeId,Name,Distance")] DistanceType distanceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distanceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distanceType);
        }

        // GET: DistanceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DistanceTypes == null)
            {
                return NotFound();
            }

            var distanceType = await _context.DistanceTypes.FindAsync(id);
            if (distanceType == null)
            {
                return NotFound();
            }
            return View(distanceType);
        }

        // POST: DistanceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DistanceTypeId,Name,Distance")] DistanceType distanceType)
        {
            if (id != distanceType.DistanceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distanceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistanceTypeExists(distanceType.DistanceTypeId))
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
            return View(distanceType);
        }

        // GET: DistanceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DistanceTypes == null)
            {
                return NotFound();
            }

            var distanceType = await _context.DistanceTypes
                .FirstOrDefaultAsync(m => m.DistanceTypeId == id);
            if (distanceType == null)
            {
                return NotFound();
            }

            return View(distanceType);
        }

        // POST: DistanceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DistanceTypes == null)
            {
                return Problem("Entity set 'MarathonContext.DistanceTypes'  is null.");
            }
            var distanceType = await _context.DistanceTypes.FindAsync(id);
            if (distanceType != null)
            {
                _context.DistanceTypes.Remove(distanceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistanceTypeExists(int id)
        {
          return (_context.DistanceTypes?.Any(e => e.DistanceTypeId == id)).GetValueOrDefault();
        }
    }
}
