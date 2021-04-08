using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLibrary.Models.User;
using UniLibraryData;
using UniLibraryData.Models;

namespace UniLibrary.Controllers
{
    public class UserController : Controller
    {
        private IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            var allUsers = _user.GetAll();

            var userModels = allUsers.Select(p => new UserDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                IdCardId = p.IdCard.Id,
                OverdueFees = p.IdCard.Fees,
                MainLibraryBranch = p.MainLibraryBranch.Name
            }).ToList();

            var model = new UserIndexModel()
            {
                Users = userModels
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var user = _user.Get(id);

            var model = new UserDetailModel
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Address = user.Adress,
                MainLibraryBranch = user.MainLibraryBranch.Name,
                MemberSince = user.IdCard.Created,
                OverdueFees = user.IdCard.Fees,
                IdCardId = user.IdCard.Id,
                Phone = user.PhoneNumber,
                KnigasCheckedOut = _user.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistories = _user.GetCheckoutHistories(id),
                Reserves = _user.GetReserves(id)
            };

            return View(model);
        }
    }
}
