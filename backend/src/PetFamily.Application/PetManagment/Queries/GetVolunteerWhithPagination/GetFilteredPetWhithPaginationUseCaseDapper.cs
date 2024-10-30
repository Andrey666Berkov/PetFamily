using System.Text;
using System.Text.Json;
using Dapper;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public class GetFilteredPetWhithPaginationUseCaseDapper :
    IQueryUSeCase<PageList<PetDto>, GetPetWhithPaginationQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger<GetFilteredPetWhithPaginationUseCaseDapper> _logger;

    public GetFilteredPetWhithPaginationUseCaseDapper(
        ISqlConnectionFactory sqlConnectionFactory,
        ILogger<GetFilteredPetWhithPaginationUseCaseDapper> logger)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
    }

    public async Task<PageList<PetDto>> Handle(
        GetPetWhithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _sqlConnectionFactory.Create();

        var parameters = new DynamicParameters();

        var sql = new StringBuilder(
            """
            SELECT id, nickName, position, files FROM pets
            """);
        
        if (string.IsNullOrEmpty(query.NickName))
        {
            sql.Append(" WHERE nickName = @NickName");
            parameters.Add("@NickName", query.NickName);
        }
        
    
        
        sql.ApplySorting(parameters ,query.SortBy, query.SortDirection);
        sql.ApplyPagination(parameters, query.Page, query.PageSize);
        
        _logger.LogInformation($"Sql: {sql.ToString()}");

        var pet = await connection.QueryAsync<PetDto, string, PetDto>(sql.ToString(),
            (pet, jsonFiles) =>
            {
                var files = JsonSerializer.Deserialize<PetFileDto[]>(jsonFiles) ?? [];
                pet.Files = files;
                return pet;
            },
            splitOn: "files",
            param: parameters);

        //можно так
        /*var pet =await connection.QueryAsync<PetDto>(
             """
             SELECT id, nickName, position, positionFrom, positionTo FROM pets
             ORDER BY position LIMIT @PageSize OFFSET @Offset
             """, new
             {
                 PageSize=query.PageSize,
                 Offset=(query.Page-1)*query.PageSize
             });
             */

        var totalCount = await connection.ExecuteScalarAsync<long>("SELECT COUNT(*) FROM pets");
        return new PageList<PetDto>
        {
            Items = pet.ToList(),
            TotalCount = totalCount,
            PageSize = query.PageSize,
            Page = query.Page
        };
    }
}

public static class AqlExtensions
{
    public static void ApplySorting(this StringBuilder sqlBuilder,
        DynamicParameters parameters,
        string? sortBy,
        string? sortDirection)
    {
        parameters.Add("@SortBy", sortBy ?? "id");
        parameters.Add("@SortDirection", sortDirection ?? "asc");
        
        sqlBuilder.Append(" ORDER BY @SortBy @SortDirection");
    }
    public static void ApplyPagination(this StringBuilder sqlBuilder,
        DynamicParameters parameters,
        int page,
        int pageSize)
    {
        parameters.Add("@PageSize", pageSize);
        parameters.Add("@Offset", (page - 1) * pageSize);
        
        sqlBuilder.Append(" LIMIT @PageSize OFFSET @Offset");
    }
}