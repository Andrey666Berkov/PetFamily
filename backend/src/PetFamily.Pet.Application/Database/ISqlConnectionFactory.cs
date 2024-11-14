using System.Data;

namespace PetFamily.Pet.Application.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}