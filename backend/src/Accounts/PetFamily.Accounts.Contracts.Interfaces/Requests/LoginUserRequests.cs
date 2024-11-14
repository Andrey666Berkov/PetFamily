using Petfamily.Accounts.Application.AccountManagment.Login;

namespace PetFamily.Accounts.Contracts.Interfaces.Requests;

public record LoginUserRequests(string Email, string Password)
{
    public LoginCommand ToCommand()=> new(Email, Password);
};