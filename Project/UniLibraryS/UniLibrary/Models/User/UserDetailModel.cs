using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLibraryData.Models;

namespace UniLibrary.Models.User
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public int IdCardId { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string Phone { get; set; }
        public string MainLibraryBranch { get; set; }
        public decimal OverdueFees { get; set; }
        public IEnumerable<Checkout> KnigasCheckedOut { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistories { get; set; }
        public IEnumerable<Reserve> Reserves { get; set; }
    }
}
