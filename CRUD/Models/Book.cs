using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public ICollection<Borrow> Borrows { get; set; }
    }
}
