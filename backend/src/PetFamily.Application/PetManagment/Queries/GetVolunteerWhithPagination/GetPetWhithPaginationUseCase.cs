using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public class GetPetWhithPaginationUseCase
{
    private readonly IReadDbContext _readDbContext;

    public GetPetWhithPaginationUseCase(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PageList<PetDto>> Handle(
        GetPetWhithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var petQuery = _readDbContext.Pets.AsNoTracking().AsQueryable();
        
        //будущая фильтрация и сортировка

        var pageList=await petQuery.ToPageList(query.Page, query.PageSize, cancellationToken);

        return pageList;
    }
}