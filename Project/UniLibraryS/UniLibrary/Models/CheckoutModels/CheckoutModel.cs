using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLibrary.Models.CheckoutModels

{
    public class CheckoutModel
    {
        public string IdCardId { get; set; }
        public string Title { get; set; }
        public int KnigaId { get; set; }
        public string ImageUrl { get; set; }
        public int HoldCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
