using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Models.Domain.Walk> AddAsync(Models.Domain.Walk walk)
        {
            //create new Id
            walk.Id = Guid.NewGuid();

            //Add to database and save changes
            await _nZWalksDbContext.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            //return new item
            return walk;
        }

        public async Task<Models.Domain.Walk> DeleteAsync(Guid id)
        {
            //Find record
            var walk = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            //If not found
            if (walk == null)
            {
                return null;
            }

            //Remove from Database
            _nZWalksDbContext.Walks.Remove(walk);

            //Save change to Database
            await _nZWalksDbContext.SaveChangesAsync();

            //Return deleted info back
            return walk;
        }

        public async Task<IEnumerable<Models.Domain.Walk>> GetAllAsync()
        {
            return await _nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Models.Domain.Walk> GetAsync(Guid id)
        {
            var walk = await _nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return walk;
        }

        public async Task<Models.Domain.Walk> UpdateAsync(Guid id, Models.Domain.Walk walk)
        {
            //Find Region
            var existingWalk = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            //Check if found
            if (existingWalk == null)
            {
                return null;
            }

            //Update Walk
            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;


            //Save changes
            //_nZWalksDbContext.Update(existingRegion); Not Needed
            await _nZWalksDbContext.SaveChangesAsync();

            //Return Region
            return existingWalk;
        }
    }
}
