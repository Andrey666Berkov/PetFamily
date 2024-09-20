using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Modules;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Modules.Entity;
using PetFamily.Domain.Shared;


namespace PetFamily.Infrastructure.Modules;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _context;

    public VolunteerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(volunteer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }
    
    public async Task<Result<Guid, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context
            .Volunteers
            .Include(pet => pet.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);
            
        if(volunteer is  null)
            return Errors.General.NotFound(volunteerId.Value);
        
        return volunteer.Id.Value;
    }
    
    public async Task<Result<Guid, Error>> GetByName(string firstName, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context
            .Volunteers
            .Include(pet => pet.Pets)
            .FirstOrDefaultAsync(v => v.FirstName == firstName, cancellationToken);
            
        if(volunteer is null)
            return Errors.General.ValueIsInavalid("volunteer");
        
        return volunteer.Id.Value;
    }
    
   
}