using petfamily.Accounts.Domain.DataModels;

namespace petfamily.Accounts.Application;

public interface ITokenProvider
{
    string GenerationAccessToken(User user);
}