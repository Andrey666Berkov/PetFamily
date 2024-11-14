using PetFamily.Core.Abstractions;

namespace petfamily.Accounts.Application.Commands;

public record LoginCommand(string Email, string Password) : ICommands;