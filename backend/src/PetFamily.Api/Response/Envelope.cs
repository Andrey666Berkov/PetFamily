using PetFamily.Domain.Shared;

namespace PetFamily.Api.Response;

public record Envelope
{
    public object? Result { get; }
    
    public List<ResponseError> Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result)
    {
        return new Envelope(result,[]);
    }
    
    public static Envelope Error(IEnumerable<ResponseError> errors)
    {
        return new Envelope(null, errors);
    }
}