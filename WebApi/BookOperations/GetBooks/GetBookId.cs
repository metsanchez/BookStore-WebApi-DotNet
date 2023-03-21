using System.Data;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookId
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public int BookId { get; set; }

        public GetBookId(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public GetIdViewModel Handle()
        {
            var book = _dbcontext.Books.Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
            {
                throw new InvalidOperationException("Book not found");
            }
            GetIdViewModel vm = _mapper.Map<GetIdViewModel>(book);
            return vm;
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