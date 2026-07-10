using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IPromotionalCodesRepository
{
    public Task AddCodeAsync(string code, int use, int interest, int quantitative, int freespins);

    public Task<List<PromotionalCodeModel>> GetAllCodesAsync();

    public Task<int?> GetCodeIdAsync(string code);
    public Task<double> GetBalanceChangeAfterActivationAsync(string code, double replenishment);
    public Task<int> GetFreeSpinsCountAfterActivationAsync(string code);
    public Task SpendCodeUseAsync(string code);
}