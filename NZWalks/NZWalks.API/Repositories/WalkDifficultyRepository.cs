using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Models.Domain.WalkDifficulty> AddAsync(Models.Domain.WalkDifficulty walkDifficulty)
        {
            //create new Id
            walkDifficulty.Id = Guid.NewGuid();

            //Add to database and save changes
            await _nZWalksDbContext.AddAsync(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();

            //return new item
            return walkDifficulty;
        }

        public async Task<Models.Domain.WalkDifficulty> DeleteAsync(Guid id)
        {
            //Find record
            var walkDifficulty = await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            //If not found
            if (walkDifficulty == null)
            {
                return null;
            }

            //Remove from Database
            _nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);

            //Save change to Database
            await _nZWalksDbContext.SaveChangesAsync();

            //Return deleted info back
            return walkDifficulty;
        }

        public async Task<IEnumerable<Models.Domain.WalkDifficulty>> GetAllAsync()
        {
            return await _nZWalksDbContext.WalkDifficulty.ToListAsync();
        }
        public async Task<Models.Domain.WalkDifficulty> GetAsync(Guid id)
        {
            var walkDifficulty = await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            return walkDifficulty;
        }

        public async Task<Models.Domain.WalkDifficulty> UpdateAsync(Guid id, Models.Domain.WalkDifficulty walkDifficulty)
        {
            //Find Region
            var existingWalkDifficulty = await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            //Check if found
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            //Update Walk
            existingWalkDifficulty.Code = walkDifficulty.Code;


            //Save changes
            await _nZWalksDbContext.SaveChangesAsync();

            //Return Region
            return existingWalkDifficulty;
        }
    }
}
