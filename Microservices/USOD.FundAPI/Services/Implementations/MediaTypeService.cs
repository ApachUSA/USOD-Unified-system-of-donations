using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class MediaTypeService : IMediaTypeService
	{
		private readonly IBaseRepository<Media_Type> _mediaTypeRepository;

		public MediaTypeService(IBaseRepository<Media_Type> mediaTypeRepository)
		{
			_mediaTypeRepository = mediaTypeRepository;
		}

		public async Task<Media_Type> CreateAsync(Media_Type mediaType)
		{
			await _mediaTypeRepository.Create(mediaType);
			return mediaType;
		}

		public async Task<bool> DeleteAsync(Media_Type mediaType)
		{
			await _mediaTypeRepository.Delete(mediaType);
			return true;
		}

		public async Task<List<Media_Type>> GetAsync()
		{
			return await _mediaTypeRepository.Get().ToListAsync();
		}

		public async Task<Media_Type?> GetByIdAsync(int mediaType_id)
		{
			return await _mediaTypeRepository.Get().FirstOrDefaultAsync(x => x.Media_Type_ID == mediaType_id);
		}

		public async Task<Media_Type> UpdateAsync(Media_Type mediaType)
		{
			await _mediaTypeRepository.Update(mediaType);
			return mediaType;
		}
	}
}
