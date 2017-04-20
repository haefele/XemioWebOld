using FluentValidation;
using Xemio.Api.Data.Models.Notes;

namespace Xemio.Api.Validators
{
    public class UpdateFolderValidator : AbstractValidator<UpdateFolder>
    {
        public UpdateFolderValidator()
        {
            this.RuleFor(f => f.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 200)
                .When(f => f.HasName());
        }
    }
}