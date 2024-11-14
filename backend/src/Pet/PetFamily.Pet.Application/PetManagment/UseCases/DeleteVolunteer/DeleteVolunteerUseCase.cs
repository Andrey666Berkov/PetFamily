using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.Extensions;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Pet.Application.PetManagment.UseCases.DeleteVolunteer;

public class DeleteVolunteerUseCase : ICommandUSeCase<Guid, DeleteVolunteerCommand>
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
    public async Task<Result<Guid, ErrorListMy>> Handler(
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
            volunteerResult.Value.Id.Value);
        
        return volunteerResult.Value.Id.Value;
    }
}