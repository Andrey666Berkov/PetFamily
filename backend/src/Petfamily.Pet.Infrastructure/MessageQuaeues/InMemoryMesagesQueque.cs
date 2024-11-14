using System.Threading.Channels;
using PetFamily.Core.Massaging;

namespace Petfamily.Pet.Infrastructure.MessageQuaeues;

public class InMemoryMesagesQueque<TMessage> :IMessageQueque<TMessage>
{
    private readonly Channel<TMessage> _channel=Channel.CreateUnbounded<TMessage>();

    public async Task WriteASync(TMessage paths, CancellationToken cancellationToken = default)
    {
       await  _channel.Writer.WriteAsync(paths);
    }
    
    public async Task<TMessage> ReadASync(CancellationToken cancellationToken = default)
    {
      return await _channel.Reader.ReadAsync(cancellationToken);
    }
}