using Petfamily.Accounts.Domain.DataModels;

namespace Petfamily.Accounts.Application;

public interface ITokenProvider
{
    string GenerationAccessToken(User user);
}