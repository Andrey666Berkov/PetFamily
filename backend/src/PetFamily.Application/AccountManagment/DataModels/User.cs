using Microsoft.AspNetCore.Identity;

namespace PetFamily.Application.AccountManagment.DataModels;

public class User : IdentityUser<Guid>
{
    
    public List<SocialNetwork> SocialNetworks { get; set; }
    public List<Role> Roles { get; set; } = [];
    
}