namespace PetFamily.Shared.Core.Abstractions;

public interface IQueryUSeCase<TResponse,in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(
        TQuery query,
        CancellationToken cancellationToken = default);
}