using CSharpFunctionalExtensions;

namespace PetFamily.Core.Dtos;

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

    public static Result<FilePath, ErrorMy> Create(Guid path, string extension)
    {
        //валидация на доступные расширения файлов
        var fullPath=path+""+extension;
        
        return new FilePath(fullPath);
    }
    public static Result<FilePath, ErrorMy> CreateOfString(string path)
    {
       return new FilePath(path);
    }
}