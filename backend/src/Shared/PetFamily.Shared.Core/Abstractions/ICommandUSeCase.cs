using CSharpFunctionalExtensions;
using PetFamily.Shared.SharedKernel;

namespace PetFamily.Shared.Core.Abstractions;

public interface ICommandUSeCase<TResponse,in TCommand> where TCommand : ICommands
{
    public Task<Result<TResponse, ErrorListMy>> Handler(
        TCommand command,
        CancellationToken cancellationToken = default);
}

public interface ICommandUSeCase<in TCommand> where TCommand : ICommands
{
    public Task<UnitResult<ErrorListMy>> Handle(
        TCommand command,
        CancellationToken cancellationToken = default);
}