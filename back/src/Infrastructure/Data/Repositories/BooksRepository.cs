using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    internal class BooksRepository : Repository<Books>, IBookRepository
    {
        public BooksRepository(SqlDbContext context) : base(context)
        {
        }

        public async Task<Books?> GetByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("O título não pode ser nulo ou vazio.", nameof(title));

            return await DbSet.FirstOrDefaultAsync(book => book.Title == title);
        }
    }
}
