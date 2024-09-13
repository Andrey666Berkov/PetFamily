using PetFamily.Domain.Enum;

namespace PetFamily.Domain.Modules;

public record Requisite
{
   private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public string Title { get;  }= default!;
    public string Description { get; }= default!;
    public static Result<Requisite> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            return Result<Requisite>.Failure("Title or description cannot be empty");
        
        return Result<Requisite>.Success(new Requisite(title, description));
    }
}