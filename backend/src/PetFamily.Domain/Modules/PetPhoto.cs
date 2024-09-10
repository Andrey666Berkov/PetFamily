﻿

namespace PetFamily.Domain.Modules;

public record PetPhoto
{
    private PetPhoto(string pathToStorage,
        bool isFavorite)
    {
        PathToStorage = pathToStorage;
        IsFavorite = isFavorite;
    }
    public string PathToStorage { get;  } = default!;
    public bool IsFavorite { get;  } = false;

    public static Result<PetPhoto> Create(string pathToStorage,
        bool isFavorite)
    {
        if(string.IsNullOrWhiteSpace(pathToStorage))
            return Result<PetPhoto>.Failure("PathToStorage cannot be empty");
        return Result<PetPhoto>.Success(new PetPhoto(pathToStorage, isFavorite)); 
    }
}