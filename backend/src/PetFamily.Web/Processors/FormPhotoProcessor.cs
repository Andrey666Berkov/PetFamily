using PetFamily.Shared.Core.Dtos;

namespace PetFamily.Web.Processors;

public class FormPhotoProcessor:IAsyncDisposable

{
    private readonly List<UploadFileDto> _photosDto =[];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            Stream photoStream=file.OpenReadStream();
            UploadFileDto uploadFileDto=new(photoStream, file.FileName);
            _photosDto.Add(uploadFileDto);
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