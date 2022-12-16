using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamClient.Models;

namespace ExamClient.Controllers
{
    public class DocumentsMVCController : Controller
    {
        private readonly DB_API_Context _context;

        public DocumentsMVCController(DB_API_Context context)
        {
            _context = context;
        }

        // GET: DocumentsMVC
        public async Task<IActionResult> Index(string id)
        {
            if (_context.Documents == null)
            {
                return Problem("It's null!");
            }

            var documents = from d in _context.Documents select d;

            if (!String.IsNullOrEmpty(id))
            {
                documents = documents.Where(d => d.Title!.Contains(id) ||
                d.Description!.Contains(id));
            }

            return View(await _context.Documents.ToListAsync());
        }

        [HttpPost]
        public string Index(string SearchQuery, bool notUsed)
        {
            return "SearchQuery: " + SearchQuery;
        }

        // GET: DocumentsMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.Document_Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: DocumentsMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentsMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Document_Id,Title,Description,Date,Path")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: DocumentsMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: DocumentsMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Document_Id,Title,Description,Date,Path")] Document document)
        {
            if (id != document.Document_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Document_Id))
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
            return View(document);
        }

        // GET: DocumentsMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.Document_Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: DocumentsMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documents == null)
            {
                return Problem("Entity set 'DB_API_Context.Documents'  is null.");
            }
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
          return _context.Documents.Any(e => e.Document_Id == id);
        }
    }
}
