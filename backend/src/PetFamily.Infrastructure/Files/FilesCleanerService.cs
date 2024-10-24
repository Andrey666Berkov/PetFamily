using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Massaging;
using FileInfo = PetFamily.Application.FileProvider.FileInfo;

namespace PetFamily.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IFilesProvider _filesProvider;
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueque<IEnumerable<FileInfo>> _messageQueque;

    public FilesCleanerService(IFilesProvider filesProvider, 
        ILogger<FilesCleanerService> logger,
        IMessageQueque<IEnumerable<FileInfo>> messageQueque)
    {
        _filesProvider = filesProvider;
        _logger = logger;
        _messageQueque = messageQueque;
    }
    
    public async Task Process(CancellationToken cancellationToken)
    {
        var fileInfos = await _messageQueque.ReadASync(cancellationToken);

        foreach (var file in fileInfos)
        {
            await _filesProvider.RemoveFiles(file, cancellationToken);
        }
    }
}