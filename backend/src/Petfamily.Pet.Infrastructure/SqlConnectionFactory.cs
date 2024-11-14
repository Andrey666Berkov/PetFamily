using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PetFamily.Pet.Application.Database;


namespace Petfamily.Pet.Infrastructure;

public class SqlConnectionFactory:ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection Create()
    {
        return new NpgsqlConnection(_configuration
            .GetConnectionString("Database"));
    }
}