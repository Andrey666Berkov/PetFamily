using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Modules.Entity;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Modules;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Guid,Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Guid,Error>> GetByName(string Name, CancellationToken cancellationToken = default);
}