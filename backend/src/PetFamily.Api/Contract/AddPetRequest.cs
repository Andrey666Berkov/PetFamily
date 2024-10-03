namespace PetFamily.Api.Contract;

public record AddPetRequest(
    IFormFileCollection Photos,
    AddressRequest Address,
    RequisiteRequest Requisite,
    string NickName,
    string Description);

public record AddressRequest(string Street, string Country, string City);
public record RequisiteRequest(string Title, string Description);
    
    