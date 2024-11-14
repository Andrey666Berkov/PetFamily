using PetFamily.Core.Abstractions;

namespace petfamily.Accounts.Application.Commands;

public record RegisterUserCommand(string Email, string UserName,string Password) : ICommands;