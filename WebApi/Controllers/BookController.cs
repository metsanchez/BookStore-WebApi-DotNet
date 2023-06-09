using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
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
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery query = new GetBooksQuery(_context, _mapper);
           return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
          GetIdViewModel result;
          try
          {
            GetBookId getid = new GetBookId(_context, _mapper);
            getid.BookId = id;
            GetByIdValidator validator = new GetByIdValidator();
            validator.ValidateAndThrow(getid);
            result = getid.Handle();
          }
          catch (Exception ex)
          {
            
            return BadRequest(ex.Message);
          }
          return Ok(result);
          
          
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
       CreateBookCommand command = new CreateBookCommand(_context, _mapper);
       try
       {
         command.Model = newBook;
         
         CreateBookValidator validator = new CreateBookValidator(); //Rules loaded.
         validator.ValidateAndThrow(command);
         command.Handle();

        //  if(!result.IsValid) 
        //  {
        //     foreach(var error in result.Errors)
        //     {
        //       Console.WriteLine("Property > " + error.PropertyName + " -  Error: " + error.ErrorMessage);
        //     }
        //  }
        //  else
        //  {
        //   command.Handle();
        //  }

         
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
         UpdateBookValidator validator = new UpdateBookValidator();
         validator.ValidateAndThrow(upcmd);
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
        DeleteBookCommand delcmd = new DeleteBookCommand(_context);
        
        try
        {
          delcmd.Handle(id);
        }
        catch (Exception ex)
        {
          
          return BadRequest(ex.Message);
        }
        return Ok();
        
    }

}
}