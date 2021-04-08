using System;
using System.Collections.Generic;
using System.Text;
using UniLibraryData.Models;

namespace UniLibraryData
{
     public interface IUser
    {
        User Get(int id);
        IEnumerable<User> GetAll();
        void Add(User newUser);

        IEnumerable<CheckoutHistory> GetCheckoutHistories(int userId);
        IEnumerable<Reserve> GetReserves(int userId);
        IEnumerable<Checkout> GetCheckouts(int userId);
    }
}
