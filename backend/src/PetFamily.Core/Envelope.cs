namespace PetFamily.Core;

public record Envelope
{
    public object? Result { get; }
    
    public ErrorList? Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result)
    {
        return new Envelope(result,null);
    }
    
    public static Envelope Error(ErrorList errors)
    {
        return new Envelope(null, errors);
    }
}