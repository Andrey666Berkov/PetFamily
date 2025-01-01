using PetFamily.Shared.SharedKernel;

namespace PetFamily.Shared.Core.Controllers;

public record Envelope
{
    public object? Result { get; }
    
    public ErrorListMy? Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, ErrorListMy? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result)
    {
        return new Envelope(result,null);
    }
    
    public static Envelope Error(ErrorListMy errors)
    {
        return new Envelope(null, errors);
    }
}