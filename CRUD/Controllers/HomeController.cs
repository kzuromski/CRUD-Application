using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models.LibraryViewModels;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Statistics(String sortOrder)
        {
            ViewData["DateSortParam"] = ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";

            IQueryable<BorrowDateGroup> data =
                 from borrow in _context.Borrows
                 group borrow by borrow.BorrowDate into dateGroup
                 select new BorrowDateGroup()
                 {
                     BorrowDate = dateGroup.Key,
                     ReaderCount = dateGroup.Count()
                 };

            switch (sortOrder)
            {
                case "date_desc":
                   data = data.OrderByDescending(b => b.BorrowDate);
                    break;

                default:
                    data = data.OrderBy(b => b.BorrowDate);
                    break;
            }
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
