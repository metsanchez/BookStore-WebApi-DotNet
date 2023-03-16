using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel updateModel {get; set;}
        private readonly BookStoreDbContext _dbcontext;
        
        public UpdateBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
            {
                throw new InvalidCastException("Kitap Bulunamadı!");
                
            }
            
            
            book.Title = updateModel.Title != default ? updateModel.Title : book.Title;
            book.PageCount = updateModel.PageCount != default ? updateModel.PageCount : book.PageCount;
            book.PublishDate = updateModel.PublishDate != default ? updateModel.PublishDate : book.PublishDate;
            book.GenreId = updateModel.GenreId != default ? updateModel.GenreId : book.GenreId;
            _dbcontext.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate {get; set;}
        public int GenreId { get; set; }

    }
}