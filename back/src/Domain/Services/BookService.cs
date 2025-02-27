using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Security.Cryptography;

namespace Domain.Services
{
    public class BookService : IService<Book>
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<ValidationResult> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetByIdAsync(long id)
        {
            var book = _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Livro não encontrado."));
                return default;
            }

            return book;

        }

        public async Task<ValidationResult> InsertAsync(Book entity)
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

        public async Task<ValidationResult> UpdateAsync(Book entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.IsValid();
            if (!entity.ValidationResult.IsValid)
                return entity.ValidationResult;
                        
            var book = await _bookRepository.GetByIdAsync(entity.Id);
            if (book == null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "Livro não encontrado."));
                return validationResult;
            }

            var exist = await _bookRepository.GetByTitleAsync(entity.Title);
            if (exist != null)
            {
                if (exist.Id != entity.Id)
                {
                    var validationResult = new ValidationResult();
                    validationResult.Errors.Add(new ValidationFailure("Title", "Já existe um livro com esse título."));
                    return validationResult;
                }
            }

            await _bookRepository.UpdateAsync(entity);
            return new ValidationResult();

        }
    }
}
