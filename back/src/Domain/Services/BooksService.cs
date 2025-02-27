using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;

namespace Domain.Services
{
    public class BooksService : IService<Books>
    {
        private readonly IBookRepository _bookRepository;

        public BooksService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<ValidationResult> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Books>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Books> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> InsertAsync(Books entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.IsValid();
            if (!entity.ValidationResult.IsValid)
                return entity.ValidationResult;

            var exists = await _bookRepository.GetByTitleAsync(entity.Title);

            if (exists != null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Title", "Já existe um livro com esse título."));
                return validationResult;
            }

            await _bookRepository.InsertAsync(entity);

            return new ValidationResult();
        }

        public Task<ValidationResult> UpdateAsync(Books entity)
        {
            throw new NotImplementedException();
        }
    }
}
