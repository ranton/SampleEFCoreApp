using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleEFCoreApp.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IAppDbContext _dbContext;

        public BooksController(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut]
        public async Task<ActionResult> ChangeWebUrl([FromQuery] string newWebUrl, CancellationToken cancellationToken)
        {
            Console.Write("New Quantum Networking WebUrl > ");

            var book = _dbContext.Books
                .Include(a => a.Author)
                .Single(b => b.Title == "Quantum Networking");
            book.Author.WebUrl = newWebUrl;
            await _dbContext.SaveChangesAsync(cancellationToken);

            Console.WriteLine("... SavedChanges called.");

            return Ok();

        }

        [HttpGet]
        public ActionResult ListAll()
        {
            var books = _dbContext.Books.AsNoTracking()
                .Include(a => a.Author)
                .ToList();

            return Ok(books);
        }


        [HttpPost]
        public async Task<ActionResult> Create(Book book, CancellationToken cancellationToken)
        {
            _dbContext.Books.Add(book);
            var id = await _dbContext.SaveChangesAsync(cancellationToken);
            return Ok(id);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var book = _dbContext.Books.Find(id);
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
