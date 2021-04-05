using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniLibraryData.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        [Required]
        public LibraryBook LibraryBook { get; set; }
        public IdCard IdCard { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
