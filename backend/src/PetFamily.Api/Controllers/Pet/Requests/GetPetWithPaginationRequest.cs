using PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

namespace PetFamily.Api.Controllers.Pet.Requests;

public record GetPetWithPaginationRequest(int Page, int PageSize)
{
    public GetPetWhithPaginationQuery ToQuery()
    {
        return new(Page, PageSize);
    }
}