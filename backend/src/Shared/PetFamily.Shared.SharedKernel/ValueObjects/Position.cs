using CSharpFunctionalExtensions;

namespace PetFamily.Shared.SharedKernel.ValueObjects;

public class Position : ValueObject
{
    public static Position First => new(1);
    public int Value { get; }

    private Position(int value)
    {
        Value = value;
    }

    public Result<Position, ErrorMy> Forward()
    {
        return Create(Value+1);
    }
    
    public  Result<Position, ErrorMy> Back()
    {
        return Create(Value-1);
    }
    public static Result<Position, ErrorMy> Create(int number)
    {
        if (number < 1)
            return ErrorsMy.General.ValueIsInavalid("serial number");
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