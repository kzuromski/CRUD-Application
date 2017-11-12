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
                new Reader{FirstName="Sebastian", LastName="Vettel", BorrowDate=DateTime.Parse("03-02-2013")},
                new Reader{FirstName="Max", LastName="Verstappen", BorrowDate=DateTime.Parse("04-01-2014")},
                new Reader{FirstName="Daniel", LastName="Riciardo", BorrowDate=DateTime.Parse("04-04-2015")},
                new Reader{FirstName="Kimi", LastName="Raikonnen", BorrowDate=DateTime.Parse("05-12-2016")},
                new Reader{FirstName="Felippe", LastName="Massa", BorrowDate=DateTime.Parse("06-05-2011")},
                new Reader{FirstName="Fernando", LastName="Alonso", BorrowDate=DateTime.Parse("07-07-2017")}
            };
            foreach(Reader r in readers)
            {
                context.Readers.Add(r);
            }
            context.SaveChanges();
            var books = new Book[]
            {
                new Book{ID=1023, Author="Bryan Danielson", Category="Drama", Title="Back to life"},
                new Book{ID=1024, Author="Robert Kubica", Category="Guide", Title="Become driver"},
                new Book{ID=1025, Author="Chris Jericho", Category="Satire", Title="Nothing happend"},
                new Book{ID=1026, Author="John Cena", Category="Comedy", Title="Just laugh"}
            };
            foreach(Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
            var borrows = new Borrow[]
            {
                new Borrow{ReaderID=1, BookID=1023},
                new Borrow{ReaderID=2, BookID=1023},
                new Borrow{ReaderID=3, BookID=1024},
                new Borrow{ReaderID=4, BookID=1025},
                new Borrow{ReaderID=5, BookID=1026},
                new Borrow{ReaderID=6, BookID=1026},
                new Borrow{ReaderID=1, BookID=1024},
                new Borrow{ReaderID=1, BookID=1026},
                new Borrow{ReaderID=1, BookID=1022},
                new Borrow{ReaderID=1, BookID=1023}
            };
            foreach(Borrow b in borrows)
            {
                context.Borrows.Add(b);
            }
            context.SaveChanges();
        }
    }
}
