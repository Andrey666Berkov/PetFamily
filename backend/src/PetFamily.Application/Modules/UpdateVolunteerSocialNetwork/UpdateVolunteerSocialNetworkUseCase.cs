using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerSocialNetwork;

public class UpdateVolunteerSocialNetworkUseCase
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerSocialNetworkUseCase> _logger;

    public UpdateVolunteerSocialNetworkUseCase(IVolunteerRepository repository, 
        ILogger<UpdateVolunteerSocialNetworkUseCase> logger )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Volunteer, Error>> Update(
        UpdateSocialNetworkCommand command
        , CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult =  _repository.GetById(volunteerId, cancellationToken).Result;
        if (volunteerResult.IsFailure)
            return  Errors.General.NotFound(command.VolunteerId);

        volunteerResult.Value.UpdateSocialNetwork(command.SocialNetworks);
        
        await _repository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updating volunteer SocialNetwork " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return  volunteerResult.Value;
        
    }
}