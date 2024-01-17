using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbContext;
        public SQLRegionRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var oldRegion = await dbContext.Regions.FirstOrDefaultAsync(r  => r.Id == id);
            if (oldRegion == null) { return null; }
            dbContext.Regions.Remove(oldRegion);
            await dbContext.SaveChangesAsync();
            return oldRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
            
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var oldRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (oldRegion == null)
            {
                return null;
            }
            oldRegion.Code = region.Code;
            oldRegion.Name = region.Name;
            oldRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return oldRegion;
        }
    }
}
