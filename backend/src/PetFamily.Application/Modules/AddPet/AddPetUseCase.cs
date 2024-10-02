using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.AddPet;

public record FiledataDtoRequest(Stream Stream, string BucketName, string ObjectName);

public class AddPetUseCase
{
    private readonly IFileProvider _fileProvider;

    public AddPetUseCase(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    //method 
    public async Task<Result<string, Error>> ProviderUseCase(
        FiledataDtoRequest request,
        CancellationToken cancellationToken = default)
    {
        var filedata = new FileDataDto(request.Stream, request.BucketName, request.ObjectName);
        return await _fileProvider.UploadFileAsync(filedata, cancellationToken);
    }
}