using DataBaseClasses;

namespace TestProject;

public abstract class DependencyOnServicesTest
{
    protected IServiceProvider _serviceProvider = null!;

    protected virtual async Task InitializeAsync()
    {
        _serviceProvider = TestServiceProvider.Provide();
    }
}
