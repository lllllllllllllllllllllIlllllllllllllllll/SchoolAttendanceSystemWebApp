using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prototype.Data;
using Prototype.Models;

namespace Prototype.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

		public IActionResult Qrcode(string code)
		{
			string url = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" + code;
            ViewBag.ImageUrl = url;

			// Return a View with the message as a parameter
			return View();
		}

		// GET: Classrooms
		public async Task<IActionResult> Index()
        {
              return View(await _context.Classrooms.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classrooms = await _context.Classrooms
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (classrooms == null)
            {
                return NotFound();
            }

            return View(classrooms);
        }

        // GET: Classrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,Code,QrCode")] Classrooms classrooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classrooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classrooms);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classrooms = await _context.Classrooms.FindAsync(id);
            if (classrooms == null)
            {
                return NotFound();
            }
            return View(classrooms);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,Code,QrCode")] Classrooms classrooms)
        {
            if (id != classrooms.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classrooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomsExists(classrooms.RoomId))
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
            return View(classrooms);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classrooms = await _context.Classrooms
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (classrooms == null)
            {
                return NotFound();
            }

            return View(classrooms);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classrooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Classrooms'  is null.");
            }
            var classrooms = await _context.Classrooms.FindAsync(id);
            if (classrooms != null)
            {
                _context.Classrooms.Remove(classrooms);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomsExists(int id)
        {
          return _context.Classrooms.Any(e => e.RoomId == id);
        }
    }
}
