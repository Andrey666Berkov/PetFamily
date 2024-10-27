
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Moq;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Massaging;
using PetFamily.Application.PetManagment;
using PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using PetFamily.Infrastructure.MessageQuaeues;
using FileInfo = PetFamily.Application.FileProvider.FileInfo;

namespace PetFamily.Application.UnitTest;

public class UploadFilesToPetTests
{
    private readonly Mock<IFilesProvider> _fileProviderMock;
    private readonly Mock<IVolunteerRepository> _volunteerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<UploadFilesToPetCommand>> _validatorMock;
    private readonly Mock<ILogger<UploadFilesPetUseCase>> _loggerMock;
    private readonly Mock<IMessageQueque<IEnumerable<FileInfo>>> _mesageQueQueMock;

    public UploadFilesToPetTests()
    {
        _fileProviderMock = new Mock<IFilesProvider>();
        _volunteerRepositoryMock = new Mock<IVolunteerRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validatorMock = new Mock<IValidator<UploadFilesToPetCommand>>();
        _mesageQueQueMock =new Mock<IMessageQueque<IEnumerable<FileInfo>>>();
       
    }
    
        
    [Fact]
    public async Task UpLoadFileUseCase_Upload_FilesToPet()
    {
        //arrange
        
        
        var ct=new CancellationTokenSource().Token;
        
        var volunteer = CreateVolunteerWithPet(1);
        
        var stream=new MemoryStream();
        var fileName = "test.jpg";

        var uploadFileDto = new UploadFileDto(stream, fileName);

        List<UploadFileDto> files = [];
        files.Add(uploadFileDto);
        
        var command=new UploadFilesToPetCommand(volunteer.Id.Value, volunteer.Pets[0].Id.Value, files);

        
                       // Mock for FileProvider
        List<FilePath> filePaths = [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,            
        ];

        _fileProviderMock
            .Setup(v => v.Handler(It.IsAny<List<FileDataDto>>(),ct))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));
        
                      // Mock for IVolunteerRepository
        _volunteerRepositoryMock
            .Setup(g => g.GetById(volunteer.Id, ct))
            .ReturnsAsync(volunteer);
        
                    // Mock for IUnitOfMork
        _unitOfWorkMock
            .Setup(g => g.SaveChanges(ct))
            .Returns(Task.CompletedTask);
        
                     // Mock for IValidatorMock
        _validatorMock
            .Setup(g => g.ValidateAsync(command, ct))
            .ReturnsAsync(new ValidationResult());
        
        /*// Mock for IMessageQueAue
        var info = new List<FileInfo>();
        _mesageQueQueMock
            .Setup(g => g.WriteASync(new List<FileInfo>(), ct))
            .ReturnsAsync();
        
        // Mock for Ilogger
        var loggerMock = new Mock<ILogger<UploadFilesPetUseCase>>();
       
        
        var useCaseResult = new UploadFilesPetUseCase(
            _fileProviderMock.Object,  
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object,
            loggerMock.Object,
            _mesageQueQueMock
            );
        
        //act
        var result=await useCaseResult.UpLoadFile(command, ct);

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(volunteer.Pets[0].Id.Value);*/
    }

    
    
    
    
    
    
    
    
    
    
    private Volunteer CreateVolunteerWithPet(int petCount)
    {
        var initial = Initials.Create("bob", "Fedorovich", "Popov").Value;
        var email=Email.Create("bob@gmail.com").Value;
        var description = "description";
        var numberPhone=PhoneNumber.Create("555-555-5555").Value;
        int experience = 10;
        var requisite = Requisite.Create("alfa","herovds").Value;
        var requisiteList = ListRequisites.Create([requisite]).Value;
        var socialNetwork = SocialNetwork.Create("VK","her").Value;
        var socialNetworkList = ListSocialNetwork.Create([socialNetwork]).Value;
        var volunteerId=VolunteerId.CreateNew();

        var address = Address.Create("city", "street", "ggg").Value;
        var pets = Enumerable.Range(1, petCount).Select(_ =>
            new Pet(
                PetId.CreateNewPetId(),
                "NameA",
                "description",
                PetType.Cat,
                "green",
                "HelfBasd",
                address,
                22,
                33,
                5555555,
                false,
                requisite,
                SpeciesBreed.Create(SpeciesId.CreateNew().Value, Guid.NewGuid()).Value,
                null
            )).ToList();

        int i = 1;
       
        foreach (var pet in pets)
        {
            pet.SetPosition(Position.Create(i++).Value);
        }
        
        var volunteer=Volunteer.Create(volunteerId, initial, email, description, 
            numberPhone, experience, requisiteList, socialNetworkList).Value;
        
        volunteer.AddPet(pets);

        return volunteer;
    }
}