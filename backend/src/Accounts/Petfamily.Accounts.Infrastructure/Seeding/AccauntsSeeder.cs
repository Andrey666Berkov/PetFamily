using Microsoft.Extensions.DependencyInjection;

namespace Petfamily.Accounts.Infrastructure.Seeding;

public class AccauntsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
   

    public AccauntsSeeder(
        IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    public async Task SeedAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var accauntSenderService = scope.ServiceProvider
            .GetRequiredService<AccountsSeederSevices>();
        
       await accauntSenderService.SeedAsync();
    }
}