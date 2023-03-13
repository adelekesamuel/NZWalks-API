using System;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace NZWalksAPI.Repositories
{
	public class RegionRepository : iRegionRepository
	{
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext )
		{
			this.nZWalksDbContext = nZWalksDbContext;

        }

        //interface implementation for iRegionRepository ***AddAsync
        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        //interface implementation for iRegionRepository ***DeleteAsync
        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        //interface implementation for iRegionRepository ***Get all region Async
        async Task<IEnumerable<Region>> iRegionRepository.GetAllAsync()
		{
			return await nZWalksDbContext.Regions.ToListAsync();
		}

		//interface implementation for iRegionRepository ***GetAsync
		Task<Region> iRegionRepository.GetAsync(Guid id)
        {
			var region = nZWalksDbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
			return region;
        }

        //interface implementation for iRegionRepository ***UpdateAsync
        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}

