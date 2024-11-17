namespace PetFamily.Core;

public record ErrorMy
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private ErrorMy(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static ErrorMy Validation(string code, string message, string? invalidField=null )
        => new ErrorMy(code, message, ErrorType.Validation, invalidField);

    public static ErrorMy NotFound(string code, string message)
        => new ErrorMy(code, message, ErrorType.NotFound);

    public static ErrorMy Failure(string code, string message)
        => new ErrorMy(code, message, ErrorType.Failure);

    public static ErrorMy Conflict(string code, string message)
        => new ErrorMy(code, message, ErrorType.Conflict);

    public  string Serialize()
    {
        return string.Join("||", Code, Message, Type);
    }

    public static ErrorMy Deserialize(string serialized)
    {
        var deserialized = serialized.Split("||");
        
        if (deserialized.Length < 3)
            throw new ArgumentException("Invalid serialized format");
        
        if(!Enum.TryParse(deserialized[2], out ErrorType errorType))
            throw new ArgumentException("Invalid serialized format");
        
        return new ErrorMy(deserialized[0], deserialized[1], errorType);
    }

    public ErrorList ToErrorList() => new([this]);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict,
    None
}