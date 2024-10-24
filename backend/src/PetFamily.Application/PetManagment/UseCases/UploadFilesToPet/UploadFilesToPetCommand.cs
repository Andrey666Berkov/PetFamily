using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagment.UseCases.UploadFilesToPet;

public record  UploadFilesToPetCommand(Guid VolunteerId, Guid PetId,IEnumerable<UploadFileDto> Files);