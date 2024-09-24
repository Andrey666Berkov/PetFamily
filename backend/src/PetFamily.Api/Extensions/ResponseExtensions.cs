using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status500InternalServerError,
            _=> StatusCodes.Status500InternalServerError
        };
        
        var responeError=new ResponseError(error.Code, error.Message, default);

        var envelop = Envelope.Error([responeError]);
             
        return new ObjectResult(envelop)
        {
            StatusCode = statusCode
        };
    }
    
    public static ActionResult ToValidationErroResponse(this ValidationResult result)
    {
        if(result.IsValid)
             throw new InvalidOperationException("Result can not to be succeed");
       
        var validationErrors = result.Errors;

        var errors = from valid in validationErrors
            let errorMessage = valid.ErrorMessage
            let error=Error.Deserialize(errorMessage)
            select new ResponseError(
                error.Code, 
                error.Message,
                valid.PropertyName);

        var evnelope = Envelope.Error(errors);
            
        return new ObjectResult(evnelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}