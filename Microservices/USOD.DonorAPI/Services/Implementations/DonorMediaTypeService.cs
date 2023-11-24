using Donor_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class DonorMediaTypeService : IDonorMediaTypeService
	{
		private readonly IBaseRepository<Donor_Media_Type> _mediaTypeRepository;

		public DonorMediaTypeService(IBaseRepository<Donor_Media_Type> mediaTypeRepository)
		{
			_mediaTypeRepository = mediaTypeRepository;
		}

		public async Task<Donor_Media_Type> CreateAsync(Donor_Media_Type mediaType)
		{
			await _mediaTypeRepository.Create(mediaType);
			return mediaType;
		}

		public async Task<bool> DeleteAsync(Donor_Media_Type mediaType)
		{
			await _mediaTypeRepository.Delete(mediaType);
			return true;
		}

		public async Task<List<Donor_Media_Type>> GetAsync()
		{
			return await _mediaTypeRepository.Get().ToListAsync();
		}

		public async Task<Donor_Media_Type?> GetByIDAsync(int mediaType_id)
		{
			return await _mediaTypeRepository.Get().FirstOrDefaultAsync(x => x.Donor_Media_Type_ID == mediaType_id);
		}

		public async Task<Donor_Media_Type> UpdateAsync(Donor_Media_Type mediaType)
		{
			return await _mediaTypeRepository.Update(mediaType);
		}
	}
}
