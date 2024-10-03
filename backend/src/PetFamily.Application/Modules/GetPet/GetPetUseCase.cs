using CSharpFunctionalExtensions;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules.GetPet;

public class GetPetUseCase
{
    private readonly IPhotosProvider _photosProvider;
    private readonly ILogger<GetPetUseCase> _logger;
   

    public GetPetUseCase(
        IPhotosProvider photosProvider)
    {
        _photosProvider = photosProvider;
    }

    public async Task<Result<string, Error>> GetPat(
        PresignedGetObjectArgsDto presignedGetObjectArgsDto,
        CancellationToken cancellationToken)
    {
        var petResult= await _photosProvider
            .GetFileAsync(presignedGetObjectArgsDto, cancellationToken);
        
        return petResult.Value;
    }
}