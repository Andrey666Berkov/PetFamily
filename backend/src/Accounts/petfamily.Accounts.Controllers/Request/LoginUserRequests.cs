using petfamily.Accounts.Application.Commands;

namespace petfamily.Accounts.Controllers.Request;

public record LoginUserRequests(string Email, string Password)
{
    public LoginCommand ToCommand()=> new(Email, Password);
};