using PetFamily.Application.AccountManagment;

namespace PetFamily.Api.Controllers.AccauntsController.Request;

public record LoginUserRequests(string Email, string Password)
{
    public LoginCommand ToCommand()=> new(Email, Password);
};