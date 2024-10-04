namespace PetFamily.Api.Contract;

public record AddPetRequest(
    IFormFileCollection Photos,
    AddressRequest Address,
    RequisiteRequest Requisite,
    string NickName,
    string Description,
    double Weight,
    int Height,
    int NumberPhone,
    bool IsCastrated);

public record AddressRequest(string Street, string Country, string City);

public record RequisiteRequest(string Title, string Description);