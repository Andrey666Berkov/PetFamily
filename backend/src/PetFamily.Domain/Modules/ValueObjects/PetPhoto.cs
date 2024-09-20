

using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record PetPhoto
{
   private PetPhoto(string pathToStorage, bool isFavorite)
    {
        PathToStorage = pathToStorage;
        IsFavorite = isFavorite;
    }
    public string PathToStorage { get;  } 
    public bool IsFavorite { get;  }

    public static Result<PetPhoto,Error> Create(string pathToStorage, bool isFavorite)
    {
        if(string.IsNullOrWhiteSpace(pathToStorage))
            return Errors.General.ValueIsInavalid(nameof(pathToStorage));
        
        return Result.Success<PetPhoto,Error>(new PetPhoto(pathToStorage, isFavorite)); 
    }
}