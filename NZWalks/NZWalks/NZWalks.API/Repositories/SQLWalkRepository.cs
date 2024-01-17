using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{   


    
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbContext;
        public SQLWalkRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
            var walk =  await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null , bool order = true,
            int pageNumber = 1 , int pageSize = 1000) //True = ascending;False = descending
        {
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = dbContext.Walks
                .Include("Difficulty")
                .Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }

            //Fitlering

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name" , StringComparison.OrdinalIgnoreCase))
                {
                    walks = order ? walks.OrderBy(w => w.Name ) : walks.OrderByDescending(w => w.Name);
                }
                else if (sortBy.Equals("LengthKM" , StringComparison.OrdinalIgnoreCase))
                {
                    walks = order ? walks.OrderBy(w => w.LengthKM) : walks.OrderByDescending(w=> w.LengthKM);
                }
            }


            //Pagination
            var skipResutls = (pageNumber -1) * pageSize;

            return await walks.Skip(skipResutls).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
           var walk =  await dbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(w => w.Id == id);
           
           return walk;
        }

        public async Task<Walk?> Update(Guid id, Walk walk)
        {
            var oldWalk =  await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (oldWalk == null) 
            {
                return null;
            }

            oldWalk.Id = id;
            oldWalk.LengthKM = walk.LengthKM; 
            oldWalk.RegionId = walk.RegionId;
            oldWalk.DifficultyId = walk.DifficultyId;   
            oldWalk.Description = walk.Description;

            await dbContext.SaveChangesAsync();

            return (oldWalk);
        }
    }
}
