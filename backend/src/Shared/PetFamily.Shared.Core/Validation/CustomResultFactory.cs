namespace PetFamily.Shared.Core.Validation;

/*public class CustomResultFactory: IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context, 
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails == null)
        {
            throw new InvalidOperationException("ValidationProblemDetails is null");
        }

        IEnumerable<ErrorMy> errorss = [];
        List<ResponseErrorValidation> errors = [];
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrors = from errorMassage in validationErrors
                let error=ErrorMy.Deserialize(errorMassage)
                select ErrorMy.Validation(
                    error.Code, 
                    error.Message,
                    invalidField);

            errorss.ToList().AddRange(responseErrors);
        }
        var errorList=new ErrorListMy(errorss);
        
        return new ObjectResult(errorList)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}*/