using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book?> GetByTitleAsync(string title);
    }
}
