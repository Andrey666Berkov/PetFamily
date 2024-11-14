using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetFamily.Core;

public class CustomResultFactory: IFluentValidationAutoValidationResultFactory
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
        List<ResponseError> errors = [];
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
        var errorList=new ErrorList(errorss);
        
        return new ObjectResult(errorList)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}