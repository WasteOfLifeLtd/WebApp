using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<Book> Books = new List<Book>();

        public void AddItem(Book book)
        {
            Book bookToAdd = Books
                .Where(p => p.ID == book.ID)
                .FirstOrDefault();

            if (bookToAdd == null)
                Books.Add(book);            
        }

        public void RemoveItem(Book book) =>
            Books.RemoveAll(x => x.ID == book.ID);

        public decimal ComputeTotalValue() => Books.Sum(e => e.Price);

        public void Clear() => Books.Clear();

        public IEnumerable<Book> GetBooksInCart { get { return Books; }  }
    }
}
