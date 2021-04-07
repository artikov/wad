using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLibraryData.Models;

namespace UniLibrary.Models.Catalog
{
    public class KnigaDetailModel
    {
        public int KnigaID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string DeweyCallNumber { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public string CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public Checkout LatestCheckout { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<KnigaReserveModel> CurrentReserves { get; set; }
    }
    public class KnigaReserveModel
    {
        public string UserName { get; set; }
        public DateTime ReservePlaced { get; set; }
    }
}
