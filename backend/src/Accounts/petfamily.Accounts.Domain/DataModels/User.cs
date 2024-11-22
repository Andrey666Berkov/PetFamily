using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace Petfamily.Accounts.Domain.DataModels;

public class User : IdentityUser<Guid>
{
    public readonly PasswordHasher<User> _passwordHasher;
    private List<Role> _roles =[];
    public IReadOnlyList<Role> Roles => _roles;

    private User()
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    public  void SetRoles(List<Role> roles)
    {
        _roles= roles;
    }
 
    public static User Create(string userName, string email, string password)
    {
        return new User
        {
            UserName = userName,
            Email = email,
            NormalizedEmail= email.ToUpper(),
          
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
           
        };
    }
}