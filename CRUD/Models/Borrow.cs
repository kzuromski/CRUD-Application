using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Borrow
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int ReaderID { get; set; }

        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
