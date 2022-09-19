//using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Models.Domain.Walk>> GetAllAsync();
        Task<Models.Domain.Walk> GetAsync(Guid id);
        Task<Models.Domain.Walk> AddAsync(Models.Domain.Walk walk);
        Task<Models.Domain.Walk> DeleteAsync(Guid id);
        Task<Models.Domain.Walk> UpdateAsync(Guid id, Models.Domain.Walk walk);
    }
}
