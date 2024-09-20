using CSharpFunctionalExtensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Modules;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Guid,Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Guid,Error>> GetByName(string Name, CancellationToken cancellationToken = default);
}