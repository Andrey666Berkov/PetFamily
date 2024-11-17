/*using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Core;
using PetFamily.Core.Dtos;
using PetFamily.Pet.Domain.Volunteers.ValueObjects;


namespace Petfamily.Pet.Infrastructure.Providers;

public class MinioProvider : IFilesProvider
{
    private const int MAX_PARALLEL = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    /// /////////////////////////
    public async Task IfBucketsNotExistCreateBucket(
        IEnumerable<string> backets,
        CancellationToken cancellationToken)
    {
        HashSet<string> buckets = [..backets];
        foreach (var busket in buckets)
        {
            var bucketexistArgs = new BucketExistsArgs().WithBucket(busket);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketexistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucetArgs = new MakeBucketArgs().WithBucket(busket);
                await _minioClient.MakeBucketAsync(makeBucetArgs, cancellationToken);
            }
        }
    }

    /// /////////////////////////
    private async Task<Result<FilePath, ErrorMy>> PutObject(
        FileDataDto fileDataDto,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileDataDto.Info.BucketName)
            .WithStreamData(fileDataDto.Stream)
            .WithObjectSize(fileDataDto.Stream.Length)
            .WithObject(fileDataDto.Info.FilePath.FullPath);
        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            return fileDataDto.Info.FilePath;
        }
        catch (Exception e)
        {
            _logger.LogError(
                e, "Fail to upload file in minio with path {path} with bucket {backet} ",
                fileDataDto.Info.FilePath.FullPath,
                fileDataDto.Info.BucketName);
            return ErrorMy.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    /// /////////////////////////
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, ErrorMy>> Handler(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default)
    {
       // return Errors.General.NotFound();
        var semaphoreSlim = new SemaphoreSlim(MAX_PARALLEL);
        var filesList = filesData.ToList();
        try
        {
            await IfBucketsNotExistCreateBucket(
                filesData.Select(x => x.Info.BucketName)
                , cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var taskresults = await Task.WhenAll(tasks);

            if (taskresults.Any(p => p.IsFailure))
                return taskresults.First().Error;

            var results = taskresults
                .Select(p => p.Value)
                .ToList();
            
            _logger.LogInformation("Upload files: {files}",results
                .Select(p=>p.FullPath));
            
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");
            return ErrorMy.Failure("FileDataDto upload failed",
                "Fail to upload file in minio");
        }
    }

   public async Task<UnitResult<ErrorMy>> RemoveFiles(
        FileInfo filesInfo,
        CancellationToken cancellationToken = default)
    { 
        try
        {
            await IfBucketsNotExistCreateBucket([filesInfo.BucketName], cancellationToken);

            var statArgs=new StatObjectArgs()
                .WithBucket(filesInfo.BucketName)
                .WithObject(filesInfo.FilePath.FullPath);
            
           var statObject= await _minioClient.StatObjectAsync(statArgs, cancellationToken);

           if (statObject == null)
               return Result.Success<ErrorMy>();
            
            var removeObjectArg=new RemoveObjectArgs()
                .WithBucket(filesInfo.BucketName)
                .WithObject(filesInfo.FilePath.FullPath);
            
            await _minioClient.RemoveObjectAsync(removeObjectArg, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to RemoveFiles in minio");
            return ErrorMy.Failure("DeleteFiles",
                "Fail to RemoveFiles in minio");
        }

        return Result.Success<ErrorMy>();

    }

    public async Task<Result<string, ErrorMy>> GetFileAsync(
        GetPetCommand getObjectCommand,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var reqParams = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "response-content-type", "application/json" }
            };

            var presignetGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(getObjectCommand.Bucket)
                .WithObject(getObjectCommand.PetId.ToString())
                .WithExpiry(1000)
                .WithHeaders(reqParams);

            string getPet = await _minioClient
                .PresignedGetObjectAsync(presignetGetObjectArgs);

            _logger.LogInformation($"Get object in minio: {getPet}");
            return getPet;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to get file in minio");
            return ErrorMy.Failure("PresignedGetObjectArgs",
                "Fail to get file in minio");
        }
    }


    public async Task<Result<string, ErrorMy>> DeletePetAsync(
        DeleteDataCommand deleteDataCommand,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(deleteDataCommand.Bucket)
                .WithObject(deleteDataCommand.PetId.ToString());

            await _minioClient.RemoveObjectAsync(args, cancellationToken);

            _logger.LogInformation($"REMOVE object in minio: {deleteDataCommand.PetId.ToString()}");
            return $"REMOVE object in minio: {deleteDataCommand.PetId.ToString()}";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to REMOVE file in minio");
            return ErrorMy.Failure("REMOVE_file_in_minio",
                "Fail to REMOVE file in minio");
        }
    }
}*/