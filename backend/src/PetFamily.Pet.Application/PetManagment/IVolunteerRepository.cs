using CSharpFunctionalExtensions;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Pet.Domain.Volunteers.IDs;
using PetFamily.Core;

namespace PetFamily.Pet.Application.PetManagment;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, ErrorMy>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, ErrorMy>> GetByName(string Name, CancellationToken cancellationToken = default);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
}