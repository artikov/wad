using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLibrary.Models.Catalog;
using UniLibrary.Models.CheckoutModels;
using UniLibraryData;

namespace UniLibrary.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryBook _knigas;
        private ICheckout _checkouts;

        public CatalogController(ILibraryBook knigas, ICheckout checkouts)
        {
            _knigas = knigas;
            _checkouts = checkouts;
        }

        public IActionResult Index()
        {
            var knigaModels = _knigas.GetAll();
            var listingResult = knigaModels
                .Select(result => new KnigaIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    Author = _knigas.GetAuthor(result.Id),
                    DeweyCallNumber = _knigas.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _knigas.GetType(result.Id)
                });

            var model = new KnigaIndexModel()
            {
                Knigas = listingResult
            };
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var kniga = _knigas.GetById(id);
            var currentReserves = _checkouts.GetCurrentReserves(id)
                .Select(a => new KnigaReserveModel
                {
                    ReservePlaced = _checkouts.GetCurrentReservePlaced(a.Id),
                    UserName = _checkouts.GetCurrentReserveUserName(a.Id)
                });

            var model = new KnigaDetailModel
            {
                KnigaID = id,
                Title = kniga.Title,
                Type = _knigas.GetType(id),
                Year = kniga.Year,
                Cost = kniga.Cost,
                Status = kniga.Status.Name,
                ImageUrl = kniga.ImageUrl,
                Author = _knigas.GetAuthor(id),
                CurrentLocation = _knigas.GetCurrentLocation(id).Name,
                DeweyCallNumber = _knigas.GetDeweyIndex(id),
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                ISBN = _knigas.GetIsbn(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                UserName = _checkouts.GetCurrentCheckoutUser(id),
                CurrentReserves =currentReserves
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var kniga = _knigas.GetById(id);

            var model = new CheckoutModel
            {
                KnigaId = id,
                ImageUrl = kniga.ImageUrl,
                Title = kniga.Title,
                IdCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };
            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id });
        }

        public IActionResult Reserve(int id)
        {
            var kniga = _knigas.GetById(id);

            var model = new CheckoutModel
            {
                KnigaId = id,
                ImageUrl = kniga.ImageUrl,
                Title = kniga.Title,
                IdCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetCurrentReserves(id).Count()
            };
            return View(model);
        }

        public IActionResult MarkLost (int knigaId)
        {
            _checkouts.MarkLost(knigaId);
            return RedirectToAction("Detail", new { id = knigaId });
        }

        public IActionResult MarkFound(int knigaId)
        {
            _checkouts.MarkFound(knigaId);
            return RedirectToAction("Detail", new { id = knigaId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int knigaId, int idCardId)
        {
            _checkouts.CheckOutItem(knigaId, idCardId);
            return RedirectToAction("Detail", new { id = knigaId });
        }

        [HttpPost]
        public IActionResult PlaceReserve(int knigaId, int idCardId)
        {
            _checkouts.PlaceReserve(knigaId, idCardId);
            return RedirectToAction("Detail", new { id = knigaId });
        }

    }
}
