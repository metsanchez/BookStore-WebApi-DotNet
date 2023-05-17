using FluentValidation;

namespace WebApi.BookOperations.GetBooks
{
    public class GetByIdValidator : AbstractValidator<GetBookId>
    {
        public GetByIdValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}