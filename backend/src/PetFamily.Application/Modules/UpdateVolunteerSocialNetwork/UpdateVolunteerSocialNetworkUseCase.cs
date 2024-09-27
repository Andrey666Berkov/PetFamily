using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
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
        UpdateSocialNetworkRequest request
        , CancellationToken cancellationToken)
    {
        var volunteerResult =  _repository.GetById(request.VolunteerId, cancellationToken).Result;
        if (volunteerResult.IsFailure)
            return  Errors.General.NotFound(request.VolunteerId);

        volunteerResult.Value.UpdateSocialNetwork(request.SocialNetworks);
        
        _logger.LogInformation("Updating volunteer SocialNetwork " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return  volunteerResult.Value;
        
    }
}