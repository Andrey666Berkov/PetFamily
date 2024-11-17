using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UploadFilesToPet;

public record  UploadFilesToPetCommand(Guid VolunteerId, Guid PetId,IEnumerable<UploadFileDto> Files) : ICommands;