﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.PetManagment;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _context;

    public VolunteerRepository(WriteDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(volunteer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<IEnumerable<Volunteer>>> GetWithPagination(
        CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.ToListAsync(cancellationToken);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken)
    {
        var volunteer = await _context
            .Volunteers
            .Include(pet => pet.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer is null)
        {
            return Errors.General.NotFound(volunteerId.Value);
        }

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByName(
        string firstName,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _context
            .Volunteers
            .Include(pet => pet.Pets)
            .FirstOrDefaultAsync(v => v.Initials.FirstName == firstName, cancellationToken);

        if (volunteer is null)
            return Errors.General.ValueIsInavalid("volunteer");

        return volunteer;
    }

    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Attach(volunteer);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _context.Remove(volunteer);
        await _context.SaveChangesAsync(cancellationToken);
        return volunteer.Id.Value;
    }
}