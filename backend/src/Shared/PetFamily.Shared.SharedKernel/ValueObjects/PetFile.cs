using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

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
    
    public static Result<PetFile, ErrorMy> Create(FilePath filePathToStorage, bool isFavorite=false)
    {
        return new PetFile(filePathToStorage, isFavorite);
    }
}