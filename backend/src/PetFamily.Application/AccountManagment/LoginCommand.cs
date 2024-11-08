using PetFamily.Application.Abstractions;

namespace PetFamily.Application.AccountManagment;

public record LoginCommand(string Email, string Password) : ICommands;