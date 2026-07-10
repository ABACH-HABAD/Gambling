using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Exceptions;
using Gambling.Infrastructure.Data;

namespace Gambling.Test;

public abstract class IntegrationDataBaseTest : DependencyOnServicesTest
{
    protected ApplicationContext _applicationContext = null!;
    protected DataBasePrepareForTests _dataBasePrepareForTests = null!;

    protected override async Task InitializeAsync(Type type = Type.Server)
    {
        await base.InitializeAsync(Type.Server);

        _applicationContext = _serviceProvider.GetRequiredService<ApplicationContext>() ?? throw new NoConnectionException();

        _dataBasePrepareForTests = new(_applicationContext, _serviceProvider);
        await ClearDataBaseAsync();
    }

    protected async Task ClearDataBaseAsync()
    {
        await _dataBasePrepareForTests.CreateTestDataBaseAsync();
        await _dataBasePrepareForTests.SeedTestDataAsync();
    }
}