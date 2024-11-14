namespace PetFamily.Shared.Core.Massaging;

public interface IMessageQueque<TMassage>
{
    Task WriteASync(TMassage paths, CancellationToken cancellationToken = default);

    Task<TMassage> ReadASync(CancellationToken cancellationToken = default);
}