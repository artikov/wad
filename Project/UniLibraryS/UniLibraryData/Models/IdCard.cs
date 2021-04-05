using System;
using System.Collections.Generic;
using System.Text;

namespace UniLibraryData.Models
{
    public class IdCard
    {
        public int Id { get; set; }

        public decimal Fees { get; set; }

        public DateTime Created { get; set; }

        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
