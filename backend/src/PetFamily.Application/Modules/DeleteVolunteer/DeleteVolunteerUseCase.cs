using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeleteVolunteer;

public class DeleteVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerUseCase> _logger;

    public DeleteVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<DeleteVolunteerUseCase> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Delete(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId);
        
        var volunteerResult=await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        
        var volunteeerID=await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Delete volunteer with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return volunteeerID;
    }
}