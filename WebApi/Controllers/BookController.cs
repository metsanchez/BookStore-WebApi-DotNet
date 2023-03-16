using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery query = new GetBooksQuery(_context);
           return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
          GetBookId getid = new GetBookId(_context);
          return Ok(getid.Handle(id));
        }

      /*  [HttpGet]
        public Book Get ([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }*/
    
    /*public void AddBook([FromBuddy] Book newBook)
    {
        var book = BookList.SingleOrDefault(x=> x.Title == newBook.Title);

        if(book is not null)
        return BadRequest();

        BookList.Add(newBook);
        return Ok();
    }*/
    [HttpPost]
    public IActionResult AddBook([FromBody]CreateBookModel newBook)
    {
       CreateBookCommand command = new CreateBookCommand(_context);
       try
       {
         command.Model = newBook;
         command.Handle();
       }
       catch (Exception ex)
       {
        
        return BadRequest(ex.Message);
       }
      
       return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody]UpdateBookModel updatedBook)
    {
        UpdateBookCommand upcmd = new UpdateBookCommand(_context);
        upcmd.BookId = id;
       try
       {
         upcmd.updateModel = updatedBook;
         upcmd.Handle();
       }
       catch (Exception ex)
       {
        
        return BadRequest(ex.Message);
       }
        return Ok();
        
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if(book is null)
        return BadRequest();

        _context.Books.Remove(book);

        _context.SaveChanges();
        return Ok();
    }

}
}