﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.PetManagment;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByName(string Name, CancellationToken cancellationToken = default);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
}