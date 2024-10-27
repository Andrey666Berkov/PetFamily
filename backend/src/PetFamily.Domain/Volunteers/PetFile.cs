using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record PetFile
{
    public FilePath FilePath { get; }
    public bool IsFavorite { get; }

    private PetFile()
    {
    }
    
    public PetFile(FilePath filePath, bool isFavorite=false)
    {
        FilePath = filePath;
        IsFavorite = isFavorite;
    }
    
    public static Result<PetFile, Error> Create(FilePath filePathToStorage, bool isFavorite=false)
    {
        return new PetFile(filePathToStorage, isFavorite);
    }
}