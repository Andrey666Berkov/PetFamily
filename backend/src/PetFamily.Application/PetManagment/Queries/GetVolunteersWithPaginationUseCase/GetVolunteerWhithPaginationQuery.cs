using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteersWithPaginationUseCase;

public record GetVolunteerWhithPaginationQuery(
    string? FirstName,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;