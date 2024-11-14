using PetFamily.Pet.Application.PetManagment.Queries.GetVolunteerWhithPagination;

namespace PetFamily.Pet.Controllers.Pet.Requests;

public record GetPetWithPaginationRequest(
    string? NickName ,
    int Page, 
    int PageSize,
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection)
{
    public GetPetWhithPaginationQuery ToQuery()
    {
        return new(
            NickName, 
            Page, 
            PageSize, 
            PositionFrom, 
            PositionTo,
            SortBy,
            SortDirection);
    }
}