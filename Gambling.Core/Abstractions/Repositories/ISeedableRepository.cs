namespace Gambling.Core.Abstractions.Repositories;

/// <summary>
/// ISeedableRepository нужен для заполнения базы данных данными по умолчанию, например ролей, типов, статусов и т.д.
/// </summary>
public interface ISeedableRepository
{
    public void SeedData();
    public Task SeedDataAsync();
}