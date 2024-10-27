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
        var petQuery = _readDbContext.Pets.AsQueryable();

        if (!string.IsNullOrWhiteSpace((query.NickName)))
        {
            var petTitleFilter = petQuery
                .Where(i => i.NickName.Contains(query.NickName));
        }


        //будущая фильтрация и сортировка

        return await petQuery.ToPageList(query.Page, query.PageSize, cancellationToken);
    }
}