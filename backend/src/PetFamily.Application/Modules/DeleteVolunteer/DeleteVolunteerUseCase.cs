using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeleteVolunteer;

public class DeleteVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<DeleteVolunteerUseCase> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, Error>> Delete(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var transaction = _unitOfWork.BeginTransaction();
        var volunteerId = VolunteerId.Create(request.VolunteerId);
        
        var volunteerResult=await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        volunteerResult.Value.Delete();
        //var volunteeerID=await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Delete volunteer with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}