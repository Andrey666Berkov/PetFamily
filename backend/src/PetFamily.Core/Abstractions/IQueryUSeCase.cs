using CSharpFunctionalExtensions;

namespace PetFamily.Core.Abstractions;

public interface ICommandUSeCase<TResponse,in TCommand> where TCommand : ICommands
{
   public Task<Result<TResponse, ErrorList>> Handler(
        TCommand command,
        CancellationToken cancellationToken = default);
}

public interface ICommandUSeCase<in TCommand> where TCommand : ICommands
{
    public  Task<UnitResult<ErrorList>> Handler(
        TCommand command,
        CancellationToken cancellationToken = default);
}


public interface IQueryUSeCase<TResponse,in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(
        TQuery query,
        CancellationToken cancellationToken = default);
}