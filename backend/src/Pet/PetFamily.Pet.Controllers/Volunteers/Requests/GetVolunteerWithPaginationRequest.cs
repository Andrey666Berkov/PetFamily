using PetFamily.Pet.Application.PetManagment.Queries.GetVolunteersWithPaginationUseCase;

namespace PetFamily.Pet.Controllers.Volunteers.Requests;

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