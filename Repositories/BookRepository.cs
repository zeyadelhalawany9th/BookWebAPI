using BookWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWebAPI.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly BookContext bookContext;

        public BookRepository(BookContext bookContext)
        {

            this.bookContext = bookContext;

        }

        public async Task<Book> Create(Book book)
        {
            bookContext.Books.Add(book);
            await bookContext.SaveChangesAsync();

            return book;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await bookContext.Books.FindAsync(id);

            if (bookToDelete != null)
            {
                bookContext.Books.Remove(bookToDelete);
                await bookContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await bookContext.Books.ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await bookContext.Books.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            bookContext.Entry(book).State = EntityState.Modified;
            await bookContext.SaveChangesAsync();
        }
    }
}

