using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IMediaTypeService
	{
		Task<List<Media_Type>> GetAsync();

		Task<Media_Type?> GetByIdAsync(int mediaType_id);

		Task<Media_Type> CreateAsync(Media_Type mediaType);

		Task<Media_Type> UpdateAsync(Media_Type mediaType);

		Task<bool> DeleteAsync(Media_Type mediaType);
	}
}
