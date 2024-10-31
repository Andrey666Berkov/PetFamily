using System.Diagnostics;
using System.Linq.Expressions;

using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.PetManagment.Queries.GetAllVolunteersWithPaginationUseCase;
using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;
using PetFamily.Domain.Shared;


namespace PetFamily.Application.PetManagment.UseCases.GetVolunteerWithPagination;

public class GetVolunteerWithPaginationUseCase :IQueryUSeCase<PageList<VolunteerDto>, GetVolunteerWhithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetVolunteerWithPaginationUseCase> _logger;
    private readonly IValidator<GetVolunteerWhithPaginationQuery> _validator;
    public GetVolunteerWithPaginationUseCase(
        IReadDbContext readDbContext,
        
        ILogger<GetVolunteerWithPaginationUseCase> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PageList<VolunteerDto>> Handle(
        GetVolunteerWhithPaginationQuery query,
        CancellationToken cancellationToken= default )
    {
        var validQuery =await _validator.ValidateAsync(query);
        if (validQuery.IsValid == false)
        {
            
        }
        var volunteers =  _readDbContext.Volunteers;
        
        if(string.IsNullOrWhiteSpace(query.FirstName)==false)
            volunteers=volunteers.Where(c=>c.Name.Contains(query.FirstName));

        
        if (string.IsNullOrWhiteSpace(query.SortBy) == false)
        {
            Expression<Func<VolunteerDto, object>> keySelector = query.SortBy switch
            {
                "first_name" => c => c.Name,
                "email" => c => c.Email,
                "phonenumber" => c => c.PhoneNumber,
                "experience" => c => c.Experience,
                _ => c => c.Id
            };
            volunteers=query.SortDirection.ToLower()=="desc"
                ? volunteers.OrderByDescending(keySelector) 
                : volunteers.OrderBy(keySelector);
        }

        return new PageList<VolunteerDto>()
        {
            Items = volunteers.ToList(),
            PageSize = query.PageSize,
            Page = query.Page,
            TotalCount = volunteers.Count()
        };
    }
}

public static class VolunteerExtensions
{
    public static IQueryable<T> WhereIfMy<T>(
        this IQueryable<T> source,
        bool operand,
        Expression<Func<T,bool>> predicate)
    {
        //метод для зарепления (дубликат) WhereIf
       return source =operand==false ? source.Where(predicate) : source;
    }
    
}