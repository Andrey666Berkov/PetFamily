using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record FilePath
{
    public string FullPath { get; }

    private FilePath()
    {
        
    }
    private FilePath(string fullFullPath)
    {
        FullPath = fullFullPath;
    }

    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        //валидация на доступные расширения файлов
        var fullPath=path+"."+extension;
        
        return new FilePath(fullPath);
    }
    public static Result<FilePath, Error> CreateOfString(string path)
    {
       return new FilePath(path);
    }
}