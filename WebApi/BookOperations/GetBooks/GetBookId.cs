using System.Data;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookId
    {
        private readonly BookStoreDbContext _dbcontext;

        public GetBookId(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<GetIdViewModel> Handle(int id)
        {
            var book = _dbcontext.Books.Where(x => x.Id == id).SingleOrDefault();
            if (book!= null)
            {
                List<GetIdViewModel> vm = new List<GetIdViewModel>();
                vm.Add( new GetIdViewModel {
                    Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                Genre = ((GenreEnum)book.GenreId).ToString()
                });
                
                return vm;
            }
            else
            {
                return null;
            }
        }

    }

    public class GetIdViewModel
    {
        
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}