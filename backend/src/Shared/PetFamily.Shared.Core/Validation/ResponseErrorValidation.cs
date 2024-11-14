namespace PetFamily.Shared.Core.Validation;

public record ResponseErrorValidation (string? ErrorCode, 
    string ErrorMessage, 
    string InvalidField);