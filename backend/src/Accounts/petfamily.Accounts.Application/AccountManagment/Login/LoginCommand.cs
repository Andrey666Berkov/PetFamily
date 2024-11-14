using PetFamily.Core.Abstractions;

namespace Petfamily.Accounts.Application.AccountManagment.Login;

public record LoginCommand(string Email, string Password) : ICommands;