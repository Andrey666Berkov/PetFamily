using FluentValidation.Results;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Shared.Core.Extensions;

public static class ValidationExtansions
{
    public static ErrorListMy ToErrorList(this ValidationResult validationResult)
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