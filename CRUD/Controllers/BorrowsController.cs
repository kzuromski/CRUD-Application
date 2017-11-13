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
    public class BorrowsController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Borrows
        public async Task<IActionResult> Index(String sortOrder)
        {
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";
            var borrows = from b in _context.Borrows
                        select b;

            switch (sortOrder)
            {
                case "date_desc":
                    borrows = borrows.OrderByDescending(b => b.BorrowDate);
                    break;
                case "date":
                    borrows = borrows.OrderBy(b => b.BorrowDate);
                    break;
                default:
                    borrows = borrows.OrderBy(b => b.BorrowDate);
                    break;
            }
            return View(await borrows.AsNoTracking().ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // GET: Borrows/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID");
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BookID,ReaderID,BorrowDate")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", borrow.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID", borrow.ReaderID);
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows.SingleOrDefaultAsync(m => m.ID == id);
            if (borrow == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", borrow.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID", borrow.ReaderID);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookID,ReaderID,BorrowDate")] Borrow borrow)
        {
            if (id != borrow.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowExists(borrow.ID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", borrow.BookID);
            ViewData["ReaderID"] = new SelectList(_context.Readers, "ID", "ID", borrow.ReaderID);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrow = await _context.Borrows.SingleOrDefaultAsync(m => m.ID == id);
            _context.Borrows.Remove(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowExists(int id)
        {
            return _context.Borrows.Any(e => e.ID == id);
        }
    }
}
