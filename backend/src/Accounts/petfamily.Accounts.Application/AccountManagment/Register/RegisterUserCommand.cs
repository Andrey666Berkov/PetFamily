using PetFamily.Core.Abstractions;

namespace Petfamily.Accounts.Application.AccountManagment.Register;

public record RegisterUserCommand(string Email, string UserName,string Password) : ICommands;