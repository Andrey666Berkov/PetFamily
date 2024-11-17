using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Pet.Domain.Volunteers.IDs;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UpdateVolunteerSocialNetwork;

public class UpdateVolunteerSocialNetworkUseCase: ICommandUSeCase<Volunteer, UpdateSocialNetworkCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerSocialNetworkUseCase> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;

    public UpdateVolunteerSocialNetworkUseCase(
        IVolunteerRepository repository, 
        ILogger<UpdateVolunteerSocialNetworkUseCase> logger,
        IValidator<UpdateSocialNetworkCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Volunteer, ErrorList>> Handler(
        UpdateSocialNetworkCommand command
        , CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid==false)
        {
            return validationResult.ToErrorList();
        } 
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult =  _repository.GetById(volunteerId, cancellationToken).Result;
        if (volunteerResult.IsFailure)
            return  ErrorsMy.General.NotFound(command.VolunteerId).ToErrorList();

        volunteerResult.Value.UpdateSocialNetwork(command.SocialNetworks);
        
        await _repository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updating volunteer SocialNetwork " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return  volunteerResult.Value;
        
    }
}