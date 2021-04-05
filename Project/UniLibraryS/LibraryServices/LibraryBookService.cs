using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UniLibraryData;
using UniLibraryData.Models;
using System.Linq;

namespace UniLibraryServices
{
    public class LibraryBookService : ILibraryBook
    {
        private UniLibraryContext _context;

        public LibraryBookService(UniLibraryContext context)
        {
            _context = context;
        }
        public void Add(LibraryBook newKniga)     // or new 'book'
        {
            _context.Add(newKniga);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryBook> GetAll()
        {
            return _context.LibraryBooks
                .Include(kniga => kniga.Status)
                .Include(kniga => kniga.Location);

        }

        public LibraryBook GetById(int id)
        {
            return 
                GetAll()
                .FirstOrDefault(kniga => kniga.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_context.Books.Any(kniga => kniga.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(kniga => kniga.Id == id).DeweyIndex;
            }

            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(kniga => kniga.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(kniga => kniga.Id == id).DeweyIndex;
            }

            else return "";
        }

        public string GetTitle(int id)
        {
            return _context.LibraryBooks
                .FirstOrDefault(a => a.Id == id)
                .Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryBooks.OfType<Book>()
                .Where(b => b.Id == id);
            return book.Any() ? "Book" : "Video";

        }

        public string GetAuthor(int id)
        {
            var isBook = _context.LibraryBooks.OfType<Book>()
                .Where(kniga => kniga.Id == id).Any();

            var isVideo = _context.LibraryBooks.OfType<Video>()
                .Where(kniga => kniga.Id == id).Any();

            return isBook ?
                _context.Books.FirstOrDefault(book => book.Id == id).Author :
                _context.Videos.FirstOrDefault(video => video.Id == id).Director
                 ?? "Unknown";
        }
    }
}
