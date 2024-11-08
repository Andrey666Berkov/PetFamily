using PetFamily.Application.AccountManagment.DataModels;

namespace PetFamily.Application.Authorization;

public interface ITokenProvider
{
    string GenerationAccessToken(User user);
}