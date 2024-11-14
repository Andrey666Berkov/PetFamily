namespace PetFamily.Pet.Application.FileProvider;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}