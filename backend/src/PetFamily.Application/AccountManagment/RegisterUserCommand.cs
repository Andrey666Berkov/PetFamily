using PetFamily.Application.Abstractions;

namespace PetFamily.Application.AccountManagment;

public record RegisterUserCommand(string Email, string UserName,string Password) : ICommands;