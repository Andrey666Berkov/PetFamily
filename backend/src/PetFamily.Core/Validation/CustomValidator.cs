using CSharpFunctionalExtensions;
using FluentValidation;

namespace PetFamily.Core.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, ErrorMy>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, ErrorMy> result = factoryMethod(value);
            if (result.IsSuccess)
                return;
            context.AddFailure(result.Error.Serialize());
        });
    }
    
    public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
        this IRuleBuilderOptions<T, TElement> rule,
        ErrorMy errorMy)
    {
     return  rule.WithMessage(errorMy.Serialize());
    }
}