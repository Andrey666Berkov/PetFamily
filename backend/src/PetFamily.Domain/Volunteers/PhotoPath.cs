using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record PhotoPath
{
    public string FullPath { get; }

    private PhotoPath()
    {
        
    }
    private PhotoPath(string fullFullPath)
    {
        FullPath = fullFullPath;
    }

    public static Result<PhotoPath, Error> Create(Guid path, string extension)
    {
        //валидация на доступные расширения файлов
        var fullPath=path+"."+extension;
        
        return new PhotoPath(fullPath);
    }
}