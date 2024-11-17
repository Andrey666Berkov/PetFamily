using FluentValidation.Results;

namespace PetFamily.Core.Extensions;

public static class ValidationExtansions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from valid in validationErrors
            let errorMessage = valid.ErrorMessage
            let error=ErrorMy.Deserialize(errorMessage)
            select  ErrorMy.Validation(
                error.Code, 
                error.Message,
                valid.PropertyName);
        return errors.ToList();
    }
}