using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.Core.Massaging;
using PetFamily.Pet.Domain.Volunteers.IDs;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;


namespace PetFamily.Pet.Application.PetManagment.UseCases.UploadFilesToPet;

public class UploadFilesPetUseCase : ICommandUSeCase<Guid, UploadFilesToPetCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly IFilesProvider _filesProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UploadFilesToPetCommand> _validator;
    private readonly ILogger<UploadFilesPetUseCase> _logger;
    private readonly IMessageQueque<IEnumerable<FileInfoMy>> _messageQueue;

    public UploadFilesPetUseCase(
        IFilesProvider filesProvider,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IValidator<UploadFilesToPetCommand> validator,
        ILogger<UploadFilesPetUseCase> logger,
        IMessageQueque<IEnumerable<FileInfoMy>> messageQueue)
    {
        _filesProvider = filesProvider;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    //method 
    public async Task<Result<Guid, ErrorList>> Handler(
        UploadFilesToPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerResult = await _volunteerRepository
            .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId);

        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();
        
        List<FileDataDto> filesData = [];
        //MINO LOAD
        foreach (var photo in command.Files)
        {
            var extension = Path.GetExtension(photo.FilePath);

            var photoPathId = Guid.NewGuid();

            var pathStorageResult = FilePath.Create(photoPathId, extension);
            if (pathStorageResult.IsFailure)
                return pathStorageResult.Error.ToErrorList();

            var photoData = new FileDataDto(
                photo.Stream,
                new Core.FileInfoMy(pathStorageResult.Value,
                BUCKET_NAME));

            filesData.Add(photoData);
        }

        var filePathResult = await _filesProvider.Handler(filesData, cancellationToken);

        if (filePathResult.IsFailure)
        {
            // записать данный о путях в Channel
            await _messageQueue.WriteASync(filesData.Select(f=>f.InfoMy), cancellationToken);
            
            return filePathResult.Error.ToErrorList();
        }

        var petFiles = filePathResult.Value
            .Select(p => new PetFile(p))
            .ToList();
        
        petResult.Value.UpdateFiles(petFiles);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Success uploaded files to pet - {id}", petResult.Value.Id.Value);
        
        return petResult.Value.Id.Value;
    }
  
}