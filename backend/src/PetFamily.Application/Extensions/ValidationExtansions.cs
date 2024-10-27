using FluentValidation.Results;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Extensions;

public static class ValidationExtansions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from valid in validationErrors
            let errorMessage = valid.ErrorMessage
            let error=Error.Deserialize(errorMessage)
            select  Error.Validation(
                error.Code, 
                error.Message,
                valid.PropertyName);
        return errors.ToList();
    }
}