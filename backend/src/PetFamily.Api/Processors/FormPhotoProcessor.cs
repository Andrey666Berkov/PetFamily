using PetFamily.Application.Modules.AddPet;

namespace PetFamily.Api.Processors;

public class FormPhotoProcessor:IAsyncDisposable

{
    private readonly List<CreateFileDto> _photosDto =[];

    public List<CreateFileDto> Process(IFormFileCollection photosRequest)
    {
        foreach (var photo in photosRequest)
        {
            Stream photoStream=photo.OpenReadStream();
            CreateFileDto createFileDto=new(photoStream, photo.FileName);
            _photosDto.Add(createFileDto);
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