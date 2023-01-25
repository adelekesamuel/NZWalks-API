﻿using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
	public interface iRegionRepository
	{
		Task<IEnumerable<Region>> GetAllAsync();

		Task<Region> GetAsync(Guid id);

		Task<Region> AddAsync(Region region);
	}
}

