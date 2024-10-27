using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public record GetPetWhithPaginationQuery(
    string? NickName, 
    /*int? PositionFrom,
    int? PositionTo,*/
    int Page, 
    int PageSize) : IQuery;