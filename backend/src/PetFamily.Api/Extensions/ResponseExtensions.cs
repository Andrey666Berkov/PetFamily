using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Response;
using PetFamily.Core;

namespace PetFamily.Api.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this ErrorMy errorMy)
    {
        var statusCode = GetStatusForErrorType(errorMy.Type);

        var envelop = Envelope.Error(errorMy.ToErrorList());
             
        return new ObjectResult(envelop)
        {
            StatusCode = statusCode
        };
    }
    
    public static ActionResult ToResponse(this ErrorList errors)
    {
        if (errors.Any() == false)
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        
        var distinctError=errors.Select(x=>x.Type).Distinct().ToList();
        
        var statusCode = distinctError.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusForErrorType(distinctError.First());
       
        var envelop = Envelope.Error(errors);
             
        return new ObjectResult(envelop)
        {
            StatusCode = statusCode
        };
    }

    public static int GetStatusForErrorType(ErrorType errorType)
    {
        switch (errorType)
        {
            case ErrorType.NotFound:
                return StatusCodes.Status404NotFound;
            case ErrorType.Conflict:
                return StatusCodes.Status409Conflict;
            case ErrorType.Failure:
                return StatusCodes.Status400BadRequest;
            case ErrorType.Validation:
            default:
                return StatusCodes.Status500InternalServerError;
        }
    }
}