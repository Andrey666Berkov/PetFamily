using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public class Position : ValueObject
{
    public static Position First => new(1);
    public int Value { get; }

    private Position(int value)
    {
        Value = value;
    }

    public Result<Position, Error> Forward()
    {
        return Create(Value+1);
    }
    
    public  Result<Position, Error> Back()
    {
        return Create(Value-1);
    }
    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
            return Errors.General.ValueIsInavalid("serial number");
        return new Position(number);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Position position)
    {
        return position.Value;
    }
    
}