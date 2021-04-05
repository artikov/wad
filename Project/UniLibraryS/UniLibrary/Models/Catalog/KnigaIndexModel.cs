using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLibrary.Models.Catalog
{
    public class KnigaIndexModel
    {
        public IEnumerable<KnigaIndexListingModel> Knigas { get; set; }
    }
}
