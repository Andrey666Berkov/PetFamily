using Microsoft.AspNetCore.Identity;

namespace Petfamily.Accounts.Domain.DataModels;

public class User : IdentityUser<Guid>
{
    
    private List<Role> _roles =[];
    public IReadOnlyList<Role> Roles => _roles;

    private User()
    {
    }
    
    public static User Create(string userName, string email, string password)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            NormalizedEmail= email.ToUpper(),
            PasswordHash = password,
        };
    }
    
    public static User CreateAdmin(string userName, string email, Role role, string password)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            _roles = [role],
            NormalizedEmail= email.ToUpper(),
            PasswordHash = password
        };
    }
}