﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record PetPhoto
{
    public FilePath FilePathToStorage { get; }
    public bool IsFavorite { get; }

    private PetPhoto()
    {
    }
    
    public PetPhoto(FilePath filePathToStorage, bool isFavorite=false)
    {
        FilePathToStorage = filePathToStorage;
        IsFavorite = isFavorite;
    }
    
    public static Result<PetPhoto, Error> Create(FilePath filePathToStorage, bool isFavorite)
    {
        return new PetPhoto(filePathToStorage, isFavorite);
    }
}