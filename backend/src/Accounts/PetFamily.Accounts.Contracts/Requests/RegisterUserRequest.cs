namespace PetFamily.Accounts.Contracts.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    /*public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(Email, UserName, Password);
    }*/
};