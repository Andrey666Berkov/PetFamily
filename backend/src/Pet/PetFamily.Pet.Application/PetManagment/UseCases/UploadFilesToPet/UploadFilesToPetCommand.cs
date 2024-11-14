using PetFamily.Shared.Core.Abstractions;
using PetFamily.Shared.Core.Dtos;

namespace PetFamily.Pet.Application.PetManagment.UseCases.UploadFilesToPet;

public record  UploadFilesToPetCommand(Guid VolunteerId, Guid PetId,IEnumerable<UploadFileDto> Files) : ICommands;