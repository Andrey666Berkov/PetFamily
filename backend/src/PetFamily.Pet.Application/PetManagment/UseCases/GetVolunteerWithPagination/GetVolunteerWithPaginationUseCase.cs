using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.Core.Volunteers;
using PetFamily.Pet.Application.PetManagment.Queries.GetVolunteersWithPaginationUseCase;

namespace PetFamily.Pet.Application.PetManagment.UseCases.GetVolunteerWithPagination;

public class GetVolunteerWithPaginationUseCase :IQueryUSeCase<PageList<VolunteerDto>, GetVolunteerWhithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetVolunteerWithPaginationUseCase> _logger;
   
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
        var volunteers =  _readDbContext.Volunteers;
        
        if(string.IsNullOrWhiteSpace(query.FirstName)==false)
            volunteers=volunteers.Where(c=>c.FirstName.Contains(query.FirstName));

        
        if (string.IsNullOrWhiteSpace(query.SortBy) == false)
        {
            Expression<Func<VolunteerDto, object>> keySelector = query.SortBy switch
            {
                "first_name" => c => c.FirstName,
                "email" => c => c.Email,
                "phonenumber" => c => c.PhoneNumber,
                "experience" => c => c.Experience,
                _ => c => c.Id
            };
            volunteers=query.SortDirection!.ToLower()=="desc"
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