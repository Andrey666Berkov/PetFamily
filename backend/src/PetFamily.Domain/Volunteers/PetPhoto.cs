using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record PetPhoto
{
    public PhotoPath PhotoPathToStorage { get; }
    public bool IsFavorite { get; }

    private PetPhoto()
    {
    }

    private PetPhoto(PhotoPath photoPathToStorage, bool isFavorite)
    {
        PhotoPathToStorage = photoPathToStorage;
        IsFavorite = isFavorite;
    }
    
    public static Result<PetPhoto, Error> Create(PhotoPath photoPathToStorage, bool isFavorite)
    {
        return new PetPhoto(photoPathToStorage, isFavorite);
    }
}