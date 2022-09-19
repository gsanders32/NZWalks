using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<Models.Domain.WalkDifficulty>> GetAllAsync();
        Task<Models.Domain.WalkDifficulty> GetAsync(Guid id);
        Task<Models.Domain.WalkDifficulty> AddAsync(Models.Domain.WalkDifficulty walkDifficulty);
        Task<Models.Domain.WalkDifficulty> DeleteAsync(Guid id);
        Task<Models.Domain.WalkDifficulty> UpdateAsync(Guid id, Models.Domain.WalkDifficulty walkDifficulty);
    }
}
