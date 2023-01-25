﻿using System;
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
    }
}

