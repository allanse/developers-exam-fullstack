using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository: IRepository<Books>
    {
        Task<Books?> GetByTitleAsync(string title);
    }
}
