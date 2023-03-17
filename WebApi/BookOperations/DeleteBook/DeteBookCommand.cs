using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book is null)
            {
                throw new InvalidOperationException("Book not found");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
           
        }

    }
}