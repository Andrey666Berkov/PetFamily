namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record AddPetRequest(
    IFormFileCollection Photos,
    AddressRequest Address,
    RequisiteRequest Requisite,
    string NickName,
    string Description,
    double Weight,
    int Height,
    int NumberPhone,
    bool IsCastrated,
    string Backet);

public record AddressRequest(string Street, string Country, string City);

public record RequisiteRequest(string Title, string Description);