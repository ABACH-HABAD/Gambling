namespace Gambling.Infrastructure.Data;

public class DataBaseConnectionString(string connectionString)
{
    public string ConnectionString { get; init; } = connectionString;

    public override string ToString() => ConnectionString;
}
