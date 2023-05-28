using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prototype.Data;
using Prototype.Models;

namespace Prototype.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

		/*
		[HttpGet("{AttendanceId}/{Date}/{Status}/{StudentId}")]
		public IActionResult QrScan([Bind("AttendanceId,Date,Status,StudentId")] Attendances attendances)
        {
            if (ModelState.IsValid)
            {
				_context.Add(attendances);
				_context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(attendances);
		}
        */
		// POST: api/students?date=2022-10-31&status=active&studentid=123
		public IActionResult Qrscan(Attendances attendance)
		{
            ViewBag.AttendanceId = attendance.AttendanceId;
            ViewBag.Date = attendance.Date;
            ViewBag.Status = attendance.Status;
            ViewBag.StudentId = attendance.StudentId;


            return View();
		}

		[HttpPost]
		public IActionResult Qrscan(DateTime date, string status, int studentid)
		{
            // create a new Student object using the params passed in [2:31 AM]
            Attendances attendance = new Attendances
            {
                AttendanceId = 2,
	            Date = date,
	            Status = status,
	            StudentId = studentid
            };

			_context.Attendances.Add(attendance); // add the new student object to the DbContext
			_context.SaveChanges(); // save changes to the database

			return View(attendance);
		}

		// GET: Attendances
		public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendances.Include(a => a.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendances == null)
            {
                return NotFound();
            }

            return View(attendances);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,Date,Status,StudentId")] Attendances attendances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", attendances.StudentId);
            return View(attendances);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", attendances.StudentId);
            return View(attendances);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,Date,Status,StudentId")] Attendances attendances)
        {
            if (id != attendances.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendancesExists(attendances.AttendanceId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", attendances.StudentId);
            return View(attendances);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendances == null)
            {
                return NotFound();
            }

            return View(attendances);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Attendances'  is null.");
            }
            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances != null)
            {
                _context.Attendances.Remove(attendances);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendancesExists(int id)
        {
          return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
