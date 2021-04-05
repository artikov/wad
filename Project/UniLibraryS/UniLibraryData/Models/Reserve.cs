using System;
using System.Collections.Generic;
using System.Text;

namespace UniLibraryData.Models
{
    public class Reserve
    {
        public int Id { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public virtual IdCard IdCard { get; set; }
        public DateTime HoldPlaced { get; set; }
    }
}
