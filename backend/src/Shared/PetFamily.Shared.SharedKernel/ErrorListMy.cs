using System.Collections;

namespace PetFamily.Shared.SharedKernel;

public class ErrorListMy : IEnumerable<ErrorMy>
{
    private readonly List<ErrorMy> _errors;

    public ErrorListMy()
    {
        _errors = [];
    }
    public ErrorListMy(IEnumerable<ErrorMy> errors)
    {
        _errors = [..errors];
    }
    
    public IEnumerator<ErrorMy> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator ErrorListMy(List<ErrorMy> errors)
    {
        return new ErrorListMy(errors);
    }
    
    public static implicit operator ErrorListMy(ErrorMy errorsMy)
    {
        return new ErrorListMy([errorsMy]);
    }
}