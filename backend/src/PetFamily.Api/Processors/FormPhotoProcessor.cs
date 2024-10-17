using PetFamily.Application.Modules.AddPet;

namespace PetFamily.Api.Processors;

public class FormPhotoProcessor:IAsyncDisposable

{
    private readonly List<CreateFileCommand> _photosDto =[];

    public List<CreateFileCommand> Process(IFormFileCollection photosRequest, string backet)
    {
        foreach (var photo in photosRequest)
        {
            Stream photoStream=photo.OpenReadStream();
            CreateFileCommand createFileCommand=new(photoStream, photo.FileName, backet);
            _photosDto.Add(createFileCommand);
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