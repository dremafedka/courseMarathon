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
    public class RegistrationsController : Controller
    {
        private readonly MarathonContext _context;

        public RegistrationsController(MarathonContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var marathonContext = _context.Registrations.Include(r => r.DistanceType).Include(r => r.Member);
            return View(await marathonContext.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.DistanceType)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            ViewData["DistanceTypeId"] = new SelectList(_context.DistanceTypes, "DistanceTypeId", "DistanceTypeId");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationId,StartTime,IntermediatePoint,FinishTime,MemberId,DistanceTypeId")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistanceTypeId"] = new SelectList(_context.DistanceTypes, "DistanceTypeId", "DistanceTypeId", registration.DistanceTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", registration.MemberId);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["DistanceTypeId"] = new SelectList(_context.DistanceTypes, "DistanceTypeId", "DistanceTypeId", registration.DistanceTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", registration.MemberId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,StartTime,IntermediatePoint,FinishTime,MemberId,DistanceTypeId")] Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
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
            ViewData["DistanceTypeId"] = new SelectList(_context.DistanceTypes, "DistanceTypeId", "DistanceTypeId", registration.DistanceTypeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", registration.MemberId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.DistanceType)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registrations == null)
            {
                return Problem("Entity set 'MarathonContext.Registrations'  is null.");
            }
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
          return (_context.Registrations?.Any(e => e.RegistrationId == id)).GetValueOrDefault();
        }
    }
}
