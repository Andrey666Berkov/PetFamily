using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Application;

public interface ITokenProvider
{
    Task<string> GenerationAccessToken(User user);
}