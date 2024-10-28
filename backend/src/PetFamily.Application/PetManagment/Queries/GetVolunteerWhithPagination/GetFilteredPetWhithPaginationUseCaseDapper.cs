using System.Text.Json;
using Dapper;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public class GetFilteredPetWhithPaginationUseCaseDapper :
    IQueryUSeCase<PageList<PetDto>, GetPetWhithPaginationQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetFilteredPetWhithPaginationUseCaseDapper(
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PageList<PetDto>> Handle(
        GetPetWhithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _sqlConnectionFactory.Create();
        
        var parameters=new DynamicParameters();
        
        var sql = """
                  SELECT id, nickName, position, files FROM pets
                  ORDER BY position LIMIT @PageSize OFFSET @Offset
                  """;
  
        parameters.Add("@PageSize", query.PageSize);
        parameters.Add("@Offset", (query.Page-1)*query.PageSize);
        
        var pet =await connection.QueryAsync<PetDto, string, PetDto>(sql,
            (pet, jsonFiles) =>
            {
                var files=JsonSerializer.Deserialize<PetFileDto[]>(jsonFiles) ?? [];
                pet.Files = files;
                return pet;
            },
            splitOn: "files",
            param:parameters);
        
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
        
        var totalCount=await connection.ExecuteScalarAsync<long>("SELECT COUNT(*) FROM pets");
        return new PageList<PetDto>
        {
            Items = pet.ToList(),
            TotalCount = totalCount,
            PageSize =  query.PageSize,
            Page = query.Page
        };
    }
}