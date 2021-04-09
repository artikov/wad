using System;
using System.Collections.Generic;
using System.Text;
using UniLibraryData.Models;

namespace UniLibraryData
{
    public interface ILibraryBranch
    {
        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<User> GetUsers(int branchId);
        IEnumerable<LibraryBook> GetLibraryBooks(int branchId);
        IEnumerable<string> GetBranchHours(int branchId);
        LibraryBranch Get(int branchId);
        void Add(LibraryBranch newBranch);
        bool IsBranchOpen(int branchId);
    }
}
