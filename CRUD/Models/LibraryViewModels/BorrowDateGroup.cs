using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models.LibraryViewModels
{
    public class BorrowDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? BorrowDate { get; set; }
        public int ReaderCount { get; set; }
    }
}
