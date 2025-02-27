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
    private readonly BooksService _bookService;

    public BookControllerr(BooksService bookService)
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

        var book = new Books(request.Title, request.Author, request.Description);

        ValidationResult result = await _bookService.InsertAsync(book);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        return Ok("Livro criado com sucesso.");
    }
}