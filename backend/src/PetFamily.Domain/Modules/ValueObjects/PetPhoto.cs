

using CSharpFunctionalExtensions;

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

    public static Result<PetPhoto> Create(string pathToStorage, bool isFavorite)
    {
        if(string.IsNullOrWhiteSpace(pathToStorage))
            return Result.Failure<PetPhoto>("PathToStorage cannot be empty");
        
        return Result.Success<PetPhoto>(new PetPhoto(pathToStorage, isFavorite)); 
    }
}