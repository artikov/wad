using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniLibraryData;
using UniLibraryData.Models;

namespace UniLibraryServices
{
    public class CheckoutService : ICheckout
    {
        private UniLibraryContext _context;
        public CheckoutService(UniLibraryContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll()
                .FirstOrDefault(checkout => checkout.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistories
                .Include(h=> h.LibraryBook)
                .Include(h=> h.IdCard)
                .Where(h => h.LibraryBook.Id == id);
        }

        public DateTime GetCurrentReservePlaced(int holdId)
        {
           return
                _context.Reserves
                .Include(h => h.LibraryBook)
                .Include(h => h.IdCard)
                .FirstOrDefault(h => h.Id == holdId)
                .HoldPlaced;
        }

        public IEnumerable<Reserve> GetCurrentReserves(int id)
        {
            return _context.Reserves
                .Include(h => h.LibraryBook)
                .Where(h => h.LibraryBook.Id == id);

        }

        public string GetCurrentReserveUserName(int holdId)
        {
            var hold = _context.Reserves
                .Include(h => h.LibraryBook)
                .Include(h => h.IdCard)
                .FirstOrDefault(h => h.Id == holdId);

            var cardId = hold?.IdCard.Id;

            var user = _context.Users
                .Include(p => p.IdCard)
                .FirstOrDefault(p => p.IdCard.Id == cardId);

            return user?.FirstName + " " + user?.LastName;
        }

        public void MarkFound(int knigaId)
        {
            var now = DateTime.Now;
            

            UpdateKnigaStatus(knigaId, "Available");
            RemoveExistingCheckouts(knigaId);
            CloseExistingCheckoutHistory(knigaId, now);

            

            _context.SaveChanges();
        }

        private void UpdateKnigaStatus(int knigaId, string newStatus)
        {
            var item = _context.LibraryBooks.
                 FirstOrDefault(a => a.Id == knigaId);

            _context.Update(item);

            item.Status = _context.Statuses
                .FirstOrDefault(status => status.Name == newStatus);
        }

        private void CloseExistingCheckoutHistory(int knigaId, DateTime now)
        {
            var history = _context.CheckoutHistories
                .FirstOrDefault(h => h.LibraryBook.Id == knigaId
                && h.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        private void RemoveExistingCheckouts(int knigaId)
        {
            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.LibraryBook.Id == knigaId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(int knigaId)
        {
            UpdateKnigaStatus(knigaId, "Lost");

            _context.SaveChanges();
        }

        public void PlaceReserve(int knigaId, int idCardId)
        {
            var now = DateTime.Now;
            var kniga = _context.LibraryBooks
                .Include(a => a.Status)
               .FirstOrDefault(a => a.Id == knigaId);

            var card = _context.IdCards
                .FirstOrDefault(c => c.Id == idCardId);

            if (kniga.Status.Name == "Available")
            {
                UpdateKnigaStatus(knigaId, "On Hold");
            }

            var hold = new Reserve
            {
                HoldPlaced = now,
                LibraryBook = kniga,
                IdCard = card
            };

            _context.Add(hold);
            _context.SaveChanges();
        }

        public void CheckInItem(int knigaId)
        {
            var now = DateTime.Now;

            var item = _context.LibraryBooks
                .FirstOrDefault(a => a.Id == knigaId);

         

            RemoveExistingCheckouts(knigaId);
            CloseExistingCheckoutHistory(knigaId, now);

            var currentReserves = _context.Reserves
                .Include(h => h.LibraryBook)
                .Include(h => h.IdCard)
                .Where(h => h.LibraryBook.Id == knigaId);


            if (currentReserves.Any())
            {
                CheckoutToEarliestReserve(knigaId, currentReserves);
                return;
            }


            UpdateKnigaStatus(knigaId, "Available");

            _context.SaveChanges();
        }

        private void CheckoutToEarliestReserve(int knigaId, IQueryable<Reserve> currentReserves)
        {
            var earliestReserve = currentReserves
                .OrderBy(reserves => reserves.HoldPlaced)
                .FirstOrDefault();

            var card = earliestReserve.IdCard;

            _context.Remove(earliestReserve);
            _context.SaveChanges();
            CheckOutItem(knigaId, card.Id);
        }

        public void CheckOutItem(int knigaId, int idCardId)
        {
            if (IsCheckedOut(knigaId))
            {
                return;
            }

            var item = _context.LibraryBooks
               .FirstOrDefault(a => a.Id == knigaId);

            UpdateKnigaStatus(knigaId, "Checked Out");

            var idCard = _context.IdCards
                .Include(card => card.Checkouts)
                .FirstOrDefault(card => card.Id == idCardId);

            var now = DateTime.Now;
            var checkout = new Checkout
            {
                LibraryBook = item,
                IdCard = idCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryBook = item,
                IdCard = idCard
            };

            _context.Add(checkoutHistory);

            _context.SaveChanges();
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int knigaId)
        {
            return _context.Checkouts
                .Where(co => co.LibraryBook.Id == knigaId)
                .Any();
        }

        public Checkout GetLatestCheckout(int knigaId)
        {
            return _context.Checkouts
                .Where(c => c.LibraryBook.Id == knigaId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public string GetCurrentCheckoutUser(int knigaId)
        {
            var checkout = GetCheckoutByKnigaId(knigaId);
            if (checkout == null)
            {
                return " ";
            };

            var cardId = checkout.IdCard.Id;
            var user = _context.Users
                .Include(p => p.IdCard)
                .FirstOrDefault(p => p.IdCard.Id == cardId);

            return user.FirstName + " " + user.LastName;

        }

        private Checkout GetCheckoutByKnigaId(int knigaId)
        {
            return _context.Checkouts
                .Include(co => co.LibraryBook)
                .Include(co => co.IdCard)
                .FirstOrDefault(co => co.LibraryBook.Id == knigaId);
        }
    }
}
