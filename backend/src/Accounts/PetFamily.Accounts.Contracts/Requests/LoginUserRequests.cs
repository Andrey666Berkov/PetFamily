namespace PetFamily.Accounts.Contracts.Requests;

public record LoginUserRequests(string Email, string Password)
{
    //public LoginCommand ToCommand()=> new(Email, Password);
};