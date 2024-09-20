using FluentValidation;

namespace PetFamily.Application.Modules.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(cvr => cvr.FirstName).Must(t =>
        {            
            if(string.IsNullOrWhiteSpace(t))
                return true;
            return false;
        });
        RuleFor(cvr => cvr.Description).NotEmpty().MaximumLength(100);
    }
}