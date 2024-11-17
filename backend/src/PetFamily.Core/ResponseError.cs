namespace PetFamily.Core;

public record ResponseError (string? ErrorCode, 
    string ErrorMessage, 
    string InvalidField);