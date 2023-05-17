using System.Data;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(command => command.updateModel.GenreId).GreaterThan(0);
            RuleFor(command => command.updateModel.PageCount).GreaterThan(0);
            RuleFor(command => command.updateModel.PublishDate.Date).NotEmpty().LessThan(DateTime.Now);
            RuleFor(command => command.updateModel.Title).NotEmpty().MinimumLength(2);
        }
    }
}