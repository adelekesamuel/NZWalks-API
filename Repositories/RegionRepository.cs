using System;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;


namespace NZWalksAPI.Repositories
{
	public class RegionRepository : iRegionRepository
	{
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext )
		{
			this.nZWalksDbContext = nZWalksDbContext;

        }

		public IEnumerable<Region> GetAll()
		{
			return nZWalksDbContext.Regions.ToList();
		}
	}
}

