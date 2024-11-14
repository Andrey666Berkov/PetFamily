using CSharpFunctionalExtensions;
using PetFamily.Core;

namespace PetFamily.Pet.Domain.Volunteers.ValueObjects;

public record PetListPhoto
{
    public IReadOnlyList<PetFile> Photos { get; }
    
    private PetListPhoto()
    {
    }
    public PetListPhoto(IEnumerable<PetFile> photos)
    {
        Photos = photos.ToList();
    }

    public static Result<PetListPhoto, ErrorMy> Create(
        IEnumerable<PetFile> photos)
    {
        var petListPhotos = new PetListPhoto(photos);
        return petListPhotos;
    }

    public static Result<PetListPhoto, ErrorMy> Empty()
    {
        var petListPhotos = new PetListPhoto();
        return petListPhotos;
    }
}