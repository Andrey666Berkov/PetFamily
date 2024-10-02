using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Infrastructure.Options;

namespace PetFamily.Api.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioClient;

    public FileController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateFile(IFormFile file, CancellationToken cancellationToken)
    {
        var bucketExistArg = new BucketExistsArgs().WithBucket("photos");
        
        var bucketExistResult = await _minioClient
            .BucketExistsAsync(bucketExistArg, cancellationToken);
        if (!bucketExistResult == false)
        {
            var makeBucketArgs=new MakeBucketArgs().WithBucket("photos");
            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }
        var path = Guid.NewGuid();
        
        using var stream = file.OpenReadStream();
        var putObjectArgs = new PutObjectArgs()
            .WithBucket("photos")
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithObject(path.ToString());
            
        var res=_minioClient
            .PutObjectAsync(putObjectArgs, cancellationToken);
        
        return Ok(res);
    }
    [HttpGet]
    public async Task<ActionResult> CreateFile(
        [FromServices] IMinioClient minioClient,
        CancellationToken cancellationToken)
    {
        var getlistMinio = 
            await minioClient.ListBucketsAsync(cancellationToken);


        var str = "";
        foreach (var n in getlistMinio.Buckets)
        {
             str=n.Name+"/n";
        }

        return Ok(str);
    }
}