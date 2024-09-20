using PetFamily.Domain.Shared;

namespace PetFamily.Api.Response;

public record Envelope
{
    public object? Result { get; }
    public string? ErrorCode { get; }   
    public string ErrorMessage { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result)
    {
        return new Envelope(result,null);
    }
    
    public static Envelope Error(Error? error)
    {
        return new Envelope(null, error );
    }
}