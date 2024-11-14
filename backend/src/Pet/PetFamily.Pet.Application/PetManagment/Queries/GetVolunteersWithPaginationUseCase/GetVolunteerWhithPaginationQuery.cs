using PetFamily.Shared.Core.Abstractions;

namespace PetFamily.Pet.Application.PetManagment.Queries.GetVolunteersWithPaginationUseCase;

public record GetVolunteerWhithPaginationQuery(
    string? FirstName,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;