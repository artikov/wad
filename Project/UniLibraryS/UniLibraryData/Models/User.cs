using System;
using System.Collections.Generic;
using System.Text;

namespace UniLibraryData.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IdCard IdCard { get; set; }
        public virtual LibraryBranch MainLibraryBranch { get; set; }
    }
}
