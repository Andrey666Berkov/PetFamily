using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Database;

public interface IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers { get;  } 
    public IQueryable<PetDto> Pets { get;  } 
}