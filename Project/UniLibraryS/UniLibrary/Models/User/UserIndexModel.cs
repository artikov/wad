using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLibrary.Models.User
{
    public class UserIndexModel
    {
        public IEnumerable<UserDetailModel> Users { get; set; }
    }
}
