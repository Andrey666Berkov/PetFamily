namespace PetFamily.Domain.Shared;

public record Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string code, string message)
        => new Error(code, message, ErrorType.Validation);

    public static Error NotFound(string code, string message)
        => new Error(code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message)
        => new Error(code, message, ErrorType.Failure);

    public static Error Conflict(string code, string message)
        => new Error(code, message, ErrorType.Conflict);

    public  string Serialize()
    {
        return string.Join("||", Code, Message, Type);
    }

    public static Error Deserialize(string serialized)
    {
        var deserialized = serialized.Split("||");
        
        if (deserialized.Length < 3)
            throw new ArgumentException("Invalid serialized format");
        
        if(!Enum.TryParse(deserialized[2], out ErrorType errorType))
            throw new ArgumentException("Invalid serialized format");
        
        return new Error(deserialized[0], deserialized[1], errorType);
    }
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict,
    None
}