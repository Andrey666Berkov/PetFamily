namespace PetFamily.Domain.Modules;

public class Result
{
    public Result(bool isSuccess, string? error)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException();

        if (isSuccess == false && error == null)
            throw new InvalidOperationException();
        
        IsSuccess = isSuccess;
        Error = error;
        
    }
    public string? Error { get; set; }
    public bool IsSuccess { get; }
    public bool IsFailure  =>!IsSuccess;
    
    public static Result Success() => new Result(true, null);
    
}

public class Result<Tvalue> : Result
{
  

  public Result(Tvalue value, bool isSuccess, string? error) : base(isSuccess, error)
  {
      _value = value;
  }
  private readonly Tvalue _value;
  public Tvalue Value => IsSuccess ? 
      _value : throw 
          new InvalidOperationException("The value of a failure result can not be accessed");
  public static Result<Tvalue> Success(Tvalue value) => 
      new Result<Tvalue>(value, true, null);
  public new static Result<Tvalue> Failure(string error) => 
      new Result<Tvalue>(default!, false, error);
}