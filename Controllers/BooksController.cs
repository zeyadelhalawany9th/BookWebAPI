using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookWebAPI.Repositories;
using BookWebAPI.Models;

namespace BookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookRepository ibookRepository;

        public BooksController(IBookRepository ibookRepository)
        {
            this.ibookRepository = ibookRepository;

        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await ibookRepository.Get();

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await ibookRepository.Get(id);

        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            var newBook = await ibookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.ID }, newBook);
        }

        [HttpPut]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] Book book)
        {
            if(id != book.ID)
            {
                return BadRequest();
            }

            await ibookRepository.Update(book);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var bookToBeDeleted = await ibookRepository.Get(id); 

            if(bookToBeDeleted == null)
            {
                return NotFound();

            }

            await ibookRepository.Delete(bookToBeDeleted.ID);

            return NoContent();

        }





    }
}
