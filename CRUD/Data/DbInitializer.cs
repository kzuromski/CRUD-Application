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
                new Reader{FirstName="Lewis", LastName="Hamilton", BorrowDate=DateTime.Parse("02-08-2017")},
                new Reader{FirstName="23", LastName="32ton", BorrowDate=DateTime.Parse("02-02-2017")}
            };
            foreach (Reader r in readers)
            {
                context.Readers.Add(r);
            }
            context.SaveChanges();
            var books = new Book[]
            {
                new Book{ID=1, Author="Nothing by Nothing", Title="Still Nothing", Category="Drama"},
                new Book{ID=2, Author="Nothing", Title="Stig", Category="Okon"}
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
            var borrows = new Borrow[]
            {
                new Borrow{BookID=1, ReaderID=1 },
                new Borrow{BookID=2, ReaderID= 1}
            };
            foreach(Borrow b in borrows)
            {
                context.Borrows.Add(b);
            }
            context.SaveChanges();
        }
    }
}
