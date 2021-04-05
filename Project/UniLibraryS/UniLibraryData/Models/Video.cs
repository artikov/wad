using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniLibraryData.Models
{
    public class Video : LibraryBook
    {
        [Required]
        public string Director { get; set; }
    }
}
