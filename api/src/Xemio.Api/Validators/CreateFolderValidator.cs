using FluentValidation;
using Xemio.Api.Data.Models.Notes;

namespace Xemio.Api.Validators
{
    public class CreateFolderValidator : AbstractValidator<CreateFolder>
    {
        public CreateFolderValidator()
        {
            this.RuleFor(f => f.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 200);
        }
    }
}
