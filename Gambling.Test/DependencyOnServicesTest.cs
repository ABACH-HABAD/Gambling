namespace Gambling.Test;

public abstract class DependencyOnServicesTest
{
    protected enum Type
    {
        Server,
        Client
    }

    protected IServiceProvider _serviceProvider = null!;

    protected virtual async Task InitializeAsync(Type type)
    {
        _serviceProvider = type switch
        {
            Type.Client => TestServiceProvider.ProvideClient(),
            Type.Server => TestServiceProvider.ProvideServer(),
            _ => throw new InvalidOperationException()
        };
    }
}
