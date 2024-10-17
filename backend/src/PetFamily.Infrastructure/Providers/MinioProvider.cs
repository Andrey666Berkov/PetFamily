﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFilesProvider
{
    private const int MAX_PARALLEL = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    /// /////////////////////////
    public async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> buckets = [..filesData.Select(x => x.BucketName)];
        foreach (var busket in buckets)
        {
            var bucketexistArgs = new BucketExistsArgs().WithBucket(busket);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketexistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucetArgs = new MakeBucketArgs().WithBucket(busket);
                await _minioClient.MakeBucketAsync(makeBucetArgs, cancellationToken);
            }
        }
    }

    /// /////////////////////////
    private async Task<Result<FilePath, Error>> PutObject(
        FileDataDto fileDataDto,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileDataDto.BucketName)
            .WithStreamData(fileDataDto.Stream)
            .WithObjectSize(fileDataDto.Stream.Length)
            .WithBucket(fileDataDto.FilePath.FullPath);
        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            return fileDataDto.FilePath;
        }
        catch (Exception e)
        {
            _logger.LogError(
                e, "Fail to upload file in minio with path {path} with bucket {backet} ",
                fileDataDto.FilePath.FullPath,
                fileDataDto.BucketName);
            return Error.Failure("file.upload", "Fail to upload file in minio");
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

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFilesAsync(
        IEnumerable<FileDataDto> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_PARALLEL);
        var filesList = filesData.ToList();
        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var taskresults = await Task.WhenAll(tasks);

            if (taskresults.Any(p => p.IsFailure))
                return taskresults.First().Error;

            var results = taskresults.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            //удаление лишних файлов
            _logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("FileDataDto upload failed",
                "Fail to upload file in minio");
        }
    }

    public async Task<Result<string, Error>> GetFileAsync(
        GetPetDto getObjectDto,
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
                .WithObject(getObjectDto.PetId.ToString())
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
            var args = new RemoveObjectArgs()
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