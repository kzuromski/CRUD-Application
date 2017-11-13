using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class ReadersController : Controller
    {
        private readonly LibraryContext _context;

        public ReadersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index(String sortOrder, 
            string searchString,
            string currentFilter,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var readers = from r in _context.Readers
                          select r;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                readers = readers.Where(r => r.LastName.Contains(searchString)
                    || r.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    readers = readers.OrderByDescending(r => r.LastName);
                    break;
                default:
                    readers = readers.OrderBy(r => r.LastName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Reader>.CreateAsync(readers.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id, String sortOrder)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .Include(s => s.Borrows)
                    .ThenInclude(e => e.Book)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName")] Reader reader)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(reader);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /*ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.SingleOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var readerToUpdate = await _context.Readers.SingleOrDefaultAsync(r => r.ID == id);
            if (await TryUpdateModelAsync<Reader>(
                readerToUpdate,
                "",
                r => r.FirstName, r => r.LastName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(readerToUpdate);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id, bool ? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Error failed";
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reader = await _context.Readers
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reader == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /*ex*/)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }

        private bool ReaderExists(int id)
        {
            return _context.Readers.Any(e => e.ID == id);
        }
    }
}
