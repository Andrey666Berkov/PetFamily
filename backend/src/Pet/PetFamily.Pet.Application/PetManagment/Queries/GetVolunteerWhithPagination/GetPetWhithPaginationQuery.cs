using PetFamily.Shared.Core.Abstractions;

namespace PetFamily.Pet.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public record GetPetWhithPaginationQuery(
    string? NickName,
    int Page,
    int PageSize,
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection) : IQuery;