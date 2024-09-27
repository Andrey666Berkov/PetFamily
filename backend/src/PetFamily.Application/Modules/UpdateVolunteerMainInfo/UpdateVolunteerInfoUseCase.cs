using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules.UpdateVolunteerMainInfo;

public class UpdateVolunteerInfoUseCase
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerInfoUseCase> _logger;

    public UpdateVolunteerInfoUseCase(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerInfoUseCase> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Update(UpdateVolunteerInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        //Находим по ID этого волонтера
        var volunteerResult=await _volunteerRepository.GetById(request.VolunteerID, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        //создаем обьект иницивлы
        var initialsResult=Initials.Create(
            request.Dto.Initials.FirstName,
            request.Dto.Initials.LastName, 
            request.Dto.Initials.MiddleName);
        
        //Меняем у волотера данные
        volunteerResult.Value.UpdateVolunteerInfo(initialsResult.Value, request.Dto.Description );
        
        var volunteeerID=await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Updated volunteer {FirstName} and {description} " +
                               "with VolunteerID: {volunteerResult.Value.Id}",
            request.Dto.Initials.FirstName, 
            request.Dto.Description,
            volunteerResult.Value.Id);
        
        return volunteeerID;
    }
}