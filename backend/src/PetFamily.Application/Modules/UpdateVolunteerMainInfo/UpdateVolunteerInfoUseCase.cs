using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public class UpdateVolunteerInfoUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerInfoUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVolunteerInfoUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerInfoUseCase> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        //Находим по ID этого волонтера
        VolunteerId volunteerId=VolunteerId.Create(request.VolunteerID);
        var volunteerResult=await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        //создаем обьект иницивлы
        var initialsResult=Initials.Create(
            request.Dto.Initials.FirstName,
            request.Dto.Initials.LastName, 
            request.Dto.Initials.MiddleName);
        
        //Меняем у волотера данные
        volunteerResult.Value.UpdateVolunteerInfo(initialsResult.Value, request.Dto.Description );
        
       // var volunteeerID=await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
       
        _logger.LogInformation("Updated volunteer {FirstName} and {description} " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            request.Dto.Initials.FirstName, 
            request.Dto.Description,
            volunteerResult.Value.Id);
        
        return volunteerResult.Value.Id.Value;
    }
}