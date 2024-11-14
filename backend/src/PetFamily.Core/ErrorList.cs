using System.Collections;

namespace PetFamily.Core;

public class ErrorList : IEnumerable<ErrorMy>
{
    private readonly List<ErrorMy> _errors;

    public ErrorList()
    {
        _errors = [];
    }
    public ErrorList(IEnumerable<ErrorMy> errors)
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

    public static implicit operator ErrorList(List<ErrorMy> errors)
    {
        return new ErrorList(errors);
    }
    
    public static implicit operator ErrorList(ErrorMy errorsMy)
    {
        return new ErrorList([errorsMy]);
    }
}