using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLibrary.Models.Catalog
{
    public class KnigaIndexListingModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string DeweyCallNumber { get; set; }
        public string NumberOfCopies { get; set; }
    }
}
