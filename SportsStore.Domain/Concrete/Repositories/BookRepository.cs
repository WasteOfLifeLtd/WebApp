using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete.Repositories
{
    public class BookRepository : IBookRepository
    {
        private AppIdentityDbContext context;

        public BookRepository(AppIdentityDbContext context)
        {
            this.context = context;
        }

        public Book DeleteBook(int bookId)
        {
            Book book = context.Books.Find(bookId);
            if (book != null)
            {
                context.Books.Remove(book);
                
            }
            return book;
        }

        public Book GetBookById(int bookId)
        {
            return context.Books.Find(bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public void InsertBook(Book book)
        {
            context.Books.Add(book);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            Book dbEntry = context.Books.Find(book.ID);
            if (dbEntry != null)
            {
                dbEntry.Title = book.Title;
                dbEntry.Description = book.Description;
                dbEntry.Price = book.Price;
                dbEntry.Category = book.Category;
                dbEntry.CoverImage = book.CoverImage;
                dbEntry.ImageMimeType = book.ImageMimeType;
            }
            context.SaveChanges(); 
        }
    }
}
