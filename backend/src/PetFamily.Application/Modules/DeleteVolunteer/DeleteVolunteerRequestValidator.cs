﻿using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeleteVolunteer;

public class DeleteVolunteerCommandValidator: AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(cvr => cvr.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
    
}