using FluentValidation;
using Xemio.Api.Data.Models.Notes;

namespace Xemio.Api.Validators
{
    public class CreateNoteValidator : AbstractValidator<CreateNote>
    {
        public CreateNoteValidator()
        {
            this.RuleFor(f => f.Title)
                .NotNull()
                .NotEmpty()
                .Length(1, 200);

            this.RuleFor(f => f.FolderId)
                .NotNull();
        }
    }
}