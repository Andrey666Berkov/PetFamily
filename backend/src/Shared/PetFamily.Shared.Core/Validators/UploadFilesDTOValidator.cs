using FluentValidation;
using PetFamily.Shared.Core.Dtos;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Shared.Core.Validators;

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