using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public class GetFilteredPetWhithPaginationUseCase : IQueryUSeCase<PageList<PetDto>, GetPetWhithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetFilteredPetWhithPaginationUseCase(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PageList<PetDto>> Handle(
        GetPetWhithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var petQuery = _readDbContext.Pets;

        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "nickname" => (issue) => issue.NickName,
            "position" => (issue) => issue.Position,
            _ => (issue) => issue.Id
        };
        
        petQuery= query.SortDirection?.ToLower() == "desc" 
            ? petQuery.OrderByDescending(keySelector) 
            : petQuery.OrderBy(keySelector);


        petQuery = petQuery
            .WhereIf(string.IsNullOrWhiteSpace((query.NickName)) == false,
                i => i.NickName.Contains(query.NickName!));

        petQuery = petQuery
            .WhereIf(query.PositionTo != null,
                i => i.Position <= query.PositionTo);

        petQuery = petQuery
            .WhereIf(query.PositionFrom != null,
                i => i.Position >= query.PositionFrom);



        //будущая фильтрация и сортировка

        return await petQuery.ToPageList(
            query.Page,
            query.PageSize,
            cancellationToken);
    }
}