using PetFamily.Application.PetManagment.UseCases.AddPet;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record AddPetRequest(
    AddressRequest Address,
    RequisiteRequest Requisite,
    string NickName ,
    string Description,
    bool IsCastrated,
    string Backet,
    double Weight=5.0,
    int Height=3,
    int NumberPhone=6)
{
    public FileDataDtoCommand CreateCommand(Guid volunteerId,
        AddressDto address, RequisiteDto requisite )
    {
        return new FileDataDtoCommand(volunteerId, address, requisite,  NickName, Description, 
            Weight, Height, NumberPhone, IsCastrated);
    }
}

public record AddressRequest(string Street, string Country, string City);

public record RequisiteRequest(string Title, string Description);