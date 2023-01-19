using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Reopsitories
{
	public interface iRegionRepository
	{
		IEnumerable<Region> GetAll();
	}
}

