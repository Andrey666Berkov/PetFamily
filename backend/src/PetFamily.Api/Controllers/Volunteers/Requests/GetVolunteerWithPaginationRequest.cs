using PetFamily.Application.PetManagment.Queries.GetAllVolunteersWithPaginationUseCase;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record GetVolunteerWithPaginationRequest(
    string? FirstName,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection)
{
    public GetVolunteerWhithPaginationQuery ToQuery()
    {
        return new(
            FirstName,
            Page,
            PageSize,
            SortBy,
            SortDirection);

    }
}