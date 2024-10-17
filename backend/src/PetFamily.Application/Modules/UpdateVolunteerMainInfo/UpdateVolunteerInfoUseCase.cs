using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public class UpdateVolunteerInfoUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerInfoUseCase> _logger;
    private readonly IValidator<UpdateVolunteerInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVolunteerInfoUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerInfoUseCase> logger,
        IValidator<UpdateVolunteerInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, ErrorList>> Update(
        UpdateVolunteerInfoCommand command,
       
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (validationResult.IsValid==false)
        {
            return validationResult.ToErrorList();
        } 
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        //Находим по ID этого волонтера
        VolunteerId volunteerId=VolunteerId.Create(command.VolunteerID);
        var volunteerResult=await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        //создаем обьект иницивлы
        var initialsResult=Initials.Create(
            command.Initials.FirstName,
            command.Initials.LastName, 
            command.Initials.MiddleName);
        
        //Меняем у волотера данные
        volunteerResult.Value.UpdateVolunteerInfo(initialsResult.Value, command.Description );
        
       // var volunteeerID=await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
       
        _logger.LogInformation("Updated volunteer {FirstName} and {description} " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            command.Initials.FirstName, 
            command.Description,
            volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}