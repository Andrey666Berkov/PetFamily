using FluentValidation;
using PetFamily.Shared.Core.Validation;
using PetFamily.Shared.SharedKernel.ValueObjects;

namespace PetFamily.Pet.Application.PetManagment.UseCases.AddPet;

public class AddPetCommandValidator: AbstractValidator<FileDataDtoCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(cvr => cvr.VolunteerId).NotEmpty();
        RuleFor(cvr => cvr.NickName).NotEmpty();
        RuleFor(cvr => cvr.Description).NotEmpty();
        RuleFor(cvr => cvr.Weigth).NotEmpty();
        RuleFor(cvr => cvr.Heigth).NotEmpty();
        RuleFor(cvr => cvr.NumberPhone).NotEmpty();
        RuleFor(cvr => cvr.Address).MustBeValueObject(
            a=>Address.Create(a.Street,a.City,a.Country));
        RuleFor(cvr => cvr.Address).MustBeValueObject(
            a=>Address.Create(a.Street,a.City,a.Country));
    }
    
}