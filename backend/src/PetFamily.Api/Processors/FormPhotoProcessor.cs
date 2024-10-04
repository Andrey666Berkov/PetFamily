using PetFamily.Application.Modules.AddPet;

namespace PetFamily.Api.Processors;

public class FormPhotoProcessor:IAsyncDisposable

{
    private readonly List<PhotoDto> _photosDto =[];

    public List<PhotoDto> Process(IFormFileCollection photosRequest)
    {
        foreach (var photo in photosRequest)
        {
            Stream photoStream=photo.OpenReadStream();
            PhotoDto photoDto=new(photoStream, photo.FileName);
            _photosDto.Add(photoDto);
        }
        return _photosDto;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var photo in _photosDto)
        {
            await photo.Stream.DisposeAsync();
        }
    }
}