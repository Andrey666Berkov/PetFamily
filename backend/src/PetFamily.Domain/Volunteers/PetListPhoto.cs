using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

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

    public static Result<PetListPhoto, Error> Create(
        IEnumerable<PetFile> photos)
    {
        var petListPhotos = new PetListPhoto(photos);
        return petListPhotos;
    }

    public static Result<PetListPhoto, Error> Empty()
    {
        var petListPhotos = new PetListPhoto();
        return petListPhotos;
    }
}