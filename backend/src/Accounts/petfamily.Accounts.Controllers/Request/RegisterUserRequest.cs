using petfamily.Accounts.Application.Commands;

namespace petfamily.Accounts.Controllers.Request;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(Email, UserName, Password);
    }
};