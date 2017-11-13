using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models;

namespace CRUD.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Readers.Any())
            {
                return;
            }
            var readers = new Reader[]
            {
                new Reader{FirstName="Sample A", LastName="Sample A"},
                new Reader{FirstName="Sample B", LastName="Sample B"}
            };
            foreach (Reader r in readers)
            {
                context.Readers.Add(r);
            }
            context.SaveChanges();
            var books = new Book[]
            {
                new Book{Author="Sample C", Title="Sample C", Category="Sample C"},
                new Book{Author="Sample D", Title="Sample D", Category="Sample D"}
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
            var borrows = new Borrow[]
            {
                new Borrow{BookID=1, ReaderID=1 , BorrowDate=DateTime.Parse("02-02-2017")},
                new Borrow{BookID=2, ReaderID=1,  BorrowDate=DateTime.Parse("02-03-2017")}
            };
            foreach(Borrow b in borrows)
            {
                context.Borrows.Add(b);
            }
            context.SaveChanges();
        }
    }
}
