using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    internal class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(SqlDbContext context) : base(context)
        {
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("O título não pode ser nulo ou vazio.", nameof(title));

            return await DbSet.FirstOrDefaultAsync(book => book.Title == title);
        }
    }
}
