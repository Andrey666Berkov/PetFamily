using PetFamily.Application.AccountManagment;

namespace PetFamily.Api.Controllers.AccauntsController.Request;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(Email, UserName, Password);
    }
};