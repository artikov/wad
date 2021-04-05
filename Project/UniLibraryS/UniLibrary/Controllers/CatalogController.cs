using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLibrary.Models.Catalog;
using UniLibraryData;

namespace UniLibrary.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryBook _knigas;

        public CatalogController(ILibraryBook knigas)
        {
            _knigas = knigas;
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
    }
}
