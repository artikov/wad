using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniLibraryData;
using UniLibraryData.Models;

namespace UniLibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        private UniLibraryContext _context;

        public LibraryBranchService(UniLibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            return GetAll()
                .FirstOrDefault(b => b.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                .Include(b => b.Users)
                .Include(b => b.LibraryBooks);
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours
                .Where(h => h.Branch.Id == branchId);

            return DataHelpers.HumanizeBusinessHours(hours);
        }

        public IEnumerable<LibraryBook> GetLibraryBooks(int branchId)
        {
            return _context.LibraryBranches
               .Include(b => b.LibraryBooks)
               .FirstOrDefault(b => b.Id == branchId)
               .LibraryBooks;
        }

        public IEnumerable<User> GetUsers(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.Users)
                .FirstOrDefault(b => b.Id == branchId)
                .Users;
        }

        public bool IsBranchOpen(int branchId)
        {
            var currentTimeHour = DateTime.Now.Hour;
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek + 1;
            var hours = _context.BranchHours
                .Where(h => h.Branch.Id == branchId);

            var daysHours = hours.FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);

            return  currentTimeHour < daysHours.CloseTime && currentTimeHour > daysHours.OpenTime;

        }
    }
}
