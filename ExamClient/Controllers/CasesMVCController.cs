﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamClient.Models;

namespace ExamClient.Controllers
{
    public class CasesMVCController : Controller
    {
        private readonly DB_API_Context _context;

        public CasesMVCController(DB_API_Context context)
        {
            _context = context;
        }

        // GET: CasesMVC
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cases.ToListAsync());
        }

        // GET: CasesMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cases == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .FirstOrDefaultAsync(m => m.Case_Id == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: CasesMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CasesMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Case_Id,Comment")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@case);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: CasesMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cases == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            return View(@case);
        }

        // POST: CasesMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Case_Id,Comment")] Case @case)
        {
            if (id != @case.Case_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.Case_Id))
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
            return View(@case);
        }

        // GET: CasesMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cases == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .FirstOrDefaultAsync(m => m.Case_Id == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: CasesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cases == null)
            {
                return Problem("Entity set 'DB_API_Context.Cases'  is null.");
            }
            var @case = await _context.Cases.FindAsync(id);
            if (@case != null)
            {
                _context.Cases.Remove(@case);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
          return _context.Cases.Any(e => e.Case_Id == id);
        }
    }
}
