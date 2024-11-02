using System.Text;
using Dapper;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Volunteers;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteersWithPaginationUseCase;

public class GetAllVolunteersWithPaginationDapperUseCase :
    IQueryUSeCase<PageList<VolunteerDto>, GetVolunteerWhithPaginationQuery>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ILogger<GetAllVolunteersWithPaginationDapperUseCase> _logger;

    public GetAllVolunteersWithPaginationDapperUseCase(
        ISqlConnectionFactory connectionFactory,
        ILogger<GetAllVolunteersWithPaginationDapperUseCase> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }


    public async Task<PageList<VolunteerDto>> Handle(
        GetVolunteerWhithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _connectionFactory.Create();

        var sqlCommand = new StringBuilder("""
                                           SELECT id, first_name, last_name, email,phone_number FROM volunteers
                                           """);
        var parameters = new DynamicParameters();

        if (string.IsNullOrEmpty(query.FirstName)==false)
        {
            sqlCommand.Append(" WHERE first_name = @FirstName");
            parameters.Add("@FirstName", query.FirstName);
        }

        sqlCommand.AddSqlSortQuery(
            parameters, 
            query.SortBy, 
            query.SortDirection);
        
        sqlCommand.AddSqlPaginatioQuery(
            parameters, 
            query.PageSize, 
            query.Page, 
            " LIMIT @PageSize OFFSET @Page");

        var volunteers = await connection
            .QueryAsync<VolunteerDto>(sqlCommand.ToString(),
                parameters);


        return new PageList<VolunteerDto>()
        {
            Items = volunteers.ToList(),
            TotalCount = volunteers.ToList().Count,
            PageSize = query.PageSize,
            Page = query.Page,
        };
    }
}

public static class SqlExtensions
{
    public static void AddSqlSortQuery(
        this StringBuilder sqlBuilder,
        DynamicParameters dynamicParameters,
        string? SortBy,
        string? SortDirection)
    {
       if (SortBy != null)
        sqlBuilder.Append($" ORDER BY {SortBy} {SortDirection}");
    }

    public static void AddSqlPaginatioQuery(this StringBuilder sqlBuilder,
        DynamicParameters dynamicParameters,
        int pageSize,
        int page,
        string sql)
    {
        dynamicParameters.Add("@PageSize", pageSize);
        dynamicParameters.Add($"@Page", (page - 1) * pageSize);
        sqlBuilder.Append(sql);
    }
}