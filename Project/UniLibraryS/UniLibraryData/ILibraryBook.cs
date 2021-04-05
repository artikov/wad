using System;
using System.Collections.Generic;
using System.Text;
using UniLibraryData.Models;

namespace UniLibraryData
{
    public interface ILibraryBook
    {
        IEnumerable<LibraryBook> GetAll();
        LibraryBook GetById(int id);
        void Add(LibraryBook newBook);
        string GetAuthor(int id);
        string GetDeweyIndex(int id);
        string GetType(int id);
        string GetTitle(int id);
        string GetIsbn(int id);

        LibraryBranch GetCurrentLocation(int id);
    }
}
