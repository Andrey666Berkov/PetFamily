using FluentValidation;
using PetFamily.Core.Dtos;
using PetFamily.Core.Validation;

namespace PetFamily.Core.Validators;

public class UploadFilesDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFilesDtoValidator()
    {
        RuleFor(u => u.FilePath)
            .NotEmpty()
            .WithError(ErrorsMy.General.ValueIsRequired());

        RuleFor(u => u.Stream).Must(c => c.Length < 5000000);

    }
}