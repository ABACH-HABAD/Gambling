
namespace DataBaseClasses.Repository.Interfaces;

public interface ISeedable
{
    public void SeedData();
    public Task SeedDataAsync();
}
