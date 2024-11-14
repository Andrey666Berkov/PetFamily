using Petfamily.Accounts.Application.AccountManagment.Register;

namespace PetFamily.Accounts.Contracts.Interfaces.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(Email, UserName, Password);
    }
};