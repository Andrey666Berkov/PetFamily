using Microsoft.Extensions.Logging;
using PetFamily.Pet.Application.FileProvider;
using PetFamily.Shared.Core.File;
using PetFamily.Shared.Core.Massaging;

namespace Petfamily.Pet.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IFilesProvider _filesProvider;
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueque<IEnumerable<FileInfoMy>> _messageQueque;

    public FilesCleanerService(IFilesProvider filesProvider, 
        ILogger<FilesCleanerService> logger,
        IMessageQueque<IEnumerable<FileInfoMy>> messageQueque)
    {
        _filesProvider = filesProvider;
        _logger = logger;
        _messageQueque = messageQueque;
    }
    
    public async Task Process(CancellationToken cancellationToken)
    {
        IEnumerable<FileInfoMy> fileInfos = await _messageQueque.ReadASync(cancellationToken);

        foreach (FileInfoMy file in fileInfos)
        {
            await _filesProvider.RemoveFiles(file, cancellationToken);
        }
    }
}