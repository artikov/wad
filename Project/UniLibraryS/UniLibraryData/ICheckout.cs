using System;
using System.Collections.Generic;
using System.Text;
using UniLibraryData.Models;

namespace UniLibraryData
{
    public interface ICheckout
    {
        void Add(Checkout newCheckout);
        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        IEnumerable<Reserve> GetCurrentReserves(int id);

        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int knigaId);
        
        DateTime GetCurrentReservePlaced(int id);
        string GetCurrentReserveUserName(int id);
        string GetCurrentCheckoutUser(int knigaId);
        bool IsCheckedOut(int id);

        void PlaceReserve(int knigaId, int idCardId);
        void CheckOutItem(int knigaId, int idCardId);
        void CheckInItem(int knigaId);
        void MarkLost(int knigaId);
        void MarkFound(int knigaId);
    }
}
