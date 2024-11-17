using PetFamily.Core.Dtos;

namespace PetFamily.Pet.Application;

public interface IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers { get;  } 
    public IQueryable<PetDto> Pets { get;  } 
}