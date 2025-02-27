using Azure.Core;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookControllerr : ControllerBase
{
    private readonly BookService _bookService;

    public BookControllerr(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet()]
    public IEnumerable<object> Get(uint page = 1, uint pageSize = 10)
    {
        List<object> books = [];

        for(int i = 0 ; i < 1000 ; i++)
            books.Add(new { Id = i, Title = $"Book {i}", Author = $"Author {i}", description = $"Book {i} - Description {i}" });

        return books.Skip((int)page * (int)pageSize).Take((int)pageSize);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookRequest request)
    {
        if (request == null)
            return BadRequest("Livro não pode ser nulo.");

        var book = new Book(request.Title, request.Author, request.Description);

        ValidationResult result = await _bookService.InsertAsync(book);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        return Ok("Livro criado com sucesso.");
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id,
                                         [FromBody] UpdateBookRequest request)
    {
        if (request == null)
            return BadRequest("Livro não pode ser nulo.");

        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return BadRequest("Livro não encontrado.");

        book.SetTitle(request.Title);
        book.SetAuthor(request.Author);
        book.SetDescription(request.Description);

        ValidationResult result = await _bookService.UpdateAsync(book);
        
        if (!result.IsValid)
            return BadRequest(result.Errors);

        return Ok("Livro atualizado com sucesso.");
    }
}