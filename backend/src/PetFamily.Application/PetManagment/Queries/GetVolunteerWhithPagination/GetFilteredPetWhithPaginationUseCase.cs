using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
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

        petQuery = petQuery.WhereIf(string.IsNullOrWhiteSpace((query.NickName)) == false,
            i => i.NickName.Contains(query.NickName!));

        petQuery = petQuery.WhereIf(query.PositionTo!=null,
            i => i.Position<=query.PositionTo);
        
        petQuery = petQuery.WhereIf(query.PositionFrom!=null,
            i => i.Position >= query.PositionFrom);

        petQuery = petQuery.OrderBy(i => i.Position);
        
        
        //будущая фильтрация и сортировка

        return await petQuery.ToPageList(
            query.Page,
            query.PageSize, 
            cancellationToken);
    }
}