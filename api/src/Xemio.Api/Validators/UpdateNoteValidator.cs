using FluentValidation;
using Xemio.Api.Data.Models.Notes;

namespace Xemio.Api.Validators
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNote>
    {
        public UpdateNoteValidator()
        {
            this.RuleFor(f => f.Title)
                .NotNull()
                .NotEmpty()
                .Length(1, 200)
                .When(f => f.HasTitle());

            this.RuleFor(f => f.FolderId)
                .NotNull()
                .When(f => f.HasFolderId());
        }
    }
}