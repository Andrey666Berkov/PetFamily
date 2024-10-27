using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Dtos.Validators;

public class UploadFilesDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFilesDtoValidator()
    {
        RuleFor(u => u.FilePath)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());

        RuleFor(u => u.Stream).Must(c => c.Length < 5000000);

    }
}