using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniLibrary.Models.Branch;
using UniLibraryData;

namespace UniLibrary.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branch;

        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index()
        {
            var branches = _branch.GetAll().Select(branch => new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                IsOpen = _branch.IsBranchOpen(branch.Id),
                NumberOfBooks = _branch.GetLibraryBooks(branch.Id).Count(),
                NumberOfUsers = _branch.GetUsers(branch.Id).Count()
            });

            var model = new BranchIndexModel()
            {
                Branches = branches
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branch.Get(id);

            var model = new BranchDetailModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone,
                OpenDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfBooks = _branch.GetLibraryBooks(id).Count(),
                NumberOfUsers = _branch.GetUsers(id).Count(),
                TotalBookValue = _branch.GetLibraryBooks(id).Sum(a => a.Cost),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branch.GetBranchHours(id)
            };

            return View(model);
        }
    }
}