using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
	public interface iRegionRepository
	{
		IEnumerable<Region> GetAll();
	}
}

