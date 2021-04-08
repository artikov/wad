using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniLibraryData;
using UniLibraryData.Models;

namespace UniLibraryServices
{
    public class UserServices : IUser
    {
        private UniLibraryContext _context;

        public UserServices(UniLibraryContext context)
        {
            _context = context;
        }
        public void Add(User newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();
        }

        public User Get(int id)
        {
            return GetAll()
                .FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(user => user.IdCard)
                .Include(user => user.MainLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int userId)
        {
            var cardId = Get(userId).IdCard.Id;

            return _context.CheckoutHistories
                .Include(co => co.IdCard)
                .Include(co => co.LibraryBook)
                .Where(co => co.IdCard.Id == cardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int userId)
        {
            var cardId = Get(userId).IdCard.Id;
                
            return _context.Checkouts
                .Include(co => co.IdCard)
                .Include(co => co.LibraryBook)
                .Where(co => co.IdCard.Id == cardId);
        }

        public IEnumerable<Reserve> GetReserves(int userId)
        {
            var cardId = Get(userId).IdCard.Id;

            return _context.Reserves
                .Include(h => h.IdCard)
                .Include(h => h.LibraryBook)
                .Where(h => h.IdCard.Id == cardId)
                .OrderByDescending(h => h.HoldPlaced);
        }
    }
}
