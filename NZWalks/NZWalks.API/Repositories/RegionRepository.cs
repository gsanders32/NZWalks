using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nZWalksDbContext.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            //Find record
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //If not found
            if (region == null)
            {
                return null;
            }
            //Remove from Database
            _nZWalksDbContext.Regions.Remove(region);
            //Save change to Database
            await _nZWalksDbContext.SaveChangesAsync();

            //Return deleted info back
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            //Find Region
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //Check if found
            if (existingRegion == null)
            {
                return null;
            }

            //Update Region
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            //Save changes
            //_nZWalksDbContext.Update(existingRegion); Not Needed
            await _nZWalksDbContext.SaveChangesAsync();

            //Return Region
            return existingRegion;
        }
    }
}
