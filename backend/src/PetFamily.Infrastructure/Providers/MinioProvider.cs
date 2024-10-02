using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> UploadFileAsync(FileDataDto fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            
            var bucketExistArg = new BucketExistsArgs().WithBucket("photos");

            var bucketExistResult = await _minioClient
                .BucketExistsAsync(bucketExistArg, cancellationToken);
            
            if (bucketExistResult == false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket("photos");
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var path = Guid.NewGuid();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket("photos")
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(path.ToString());

            var res = await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return res.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("FileDataDto upload failed",
                "Fail to upload file in minio");
        }
    }

    public async Task<Result<string, Error>> GetFileAsync(
        PresignedGetObjectArgsDto getObjectDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var reqParams = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "response-content-type", "application/json" }
            };

            var presignetGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(getObjectDto.Bucket)
                .WithObject(getObjectDto.Id.ToString())
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
            return Error.Failure("PresignedGetObjectArgs", 
                "Fail to get file in minio");
        }
    }
    
    
    public async Task<Result<string, Error>> DeletePetAsync(
        DeleteDataDto deleteDataDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args=new RemoveObjectArgs()
                .WithBucket(deleteDataDto.Bucket)
                .WithObject(deleteDataDto.PetId.ToString());
            
         await _minioClient.RemoveObjectAsync(args, cancellationToken);
            
            _logger.LogInformation($"REMOVE object in minio: {deleteDataDto.PetId.ToString()}");
            return $"REMOVE object in minio: {deleteDataDto.PetId.ToString()}";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to REMOVE file in minio");
            return Error.Failure("REMOVE_file_in_minio", 
                "Fail to REMOVE file in minio");
        }
    }
}


