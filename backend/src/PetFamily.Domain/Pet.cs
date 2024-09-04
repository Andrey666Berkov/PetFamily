using PetFamily.Domain.Enum;

namespace PetFamily.Domain;

public class Pet
{
    public Guid Id { get; set; }
    public string NickName { get; set; }= default!;
    public PetType PetType { get; set; }
    public string Discription { get; set; }= default!;
    public string Breed { get; set; }= default!;
    public string Color { get; set; } = default!;
    public string InfoHelth { get; set; }= default!;
    public string Address { get; set; }= default!;
    public double Weight { get; set; }
    public int Height { get; set; }
    public int NumberPhoneOwner { get; set; }
    public Castrated Castrated { get; set; }   
    public DateOnly BirthDate { get; set; }
    public Vaccinated Vaccinated { get; set; }
    public StatusHelper StatusHelper { get; set; }
    public Requisites Requisites { get; set; }= default!;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
}