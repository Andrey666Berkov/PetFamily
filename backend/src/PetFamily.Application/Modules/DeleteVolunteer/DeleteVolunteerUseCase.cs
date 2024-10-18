using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.DeleteVolunteer;

public class DeleteVolunteerUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerUseCase> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<DeleteVolunteerUseCase> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Delete(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid==false)
        {
            return validationResult.ToErrorList();
        } 
        
        var transaction = _unitOfWork.BeginTransaction();
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult=await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        volunteerResult.Value.Delete();
        //var volunteeerID=await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Delete volunteer with VolunteerID: {volunteerResult.Value.Id}",
            volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}