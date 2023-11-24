using Donor_Library.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class DonorMediaService : IDonorMediaService
	{
		private readonly IBaseRepository<Donor_Media> _donorMediaRepository;

		public DonorMediaService(IBaseRepository<Donor_Media> donorMediaRepository)
		{
			_donorMediaRepository = donorMediaRepository;
		}

		public async Task<Donor_Media> CreateAsync(Donor_Media media)
		{
			await _donorMediaRepository.Create(media);
			return media;
		}

		public async Task<List<Donor_Media>> CreateAsync(List<Donor_Media> media)
		{
			await _donorMediaRepository.Create(media);
			return media;
		}

		public async Task<bool> DeleteAsync(Donor_Media media)
		{
			await _donorMediaRepository.Delete(media);
			return true;
		}

		public async Task<List<Donor_Media>> GetAsync()
		{
			return await _donorMediaRepository.Get().ToListAsync();
		}

		public async Task<Donor_Media?> GetByIDAsync(int media_id)
		{
			return await _donorMediaRepository.Get().FirstOrDefaultAsync(x => x.Donor_Media_ID == media_id);
		}

		public async Task<Donor_Media> UpdateAsync(Donor_Media media)
		{
			return await _donorMediaRepository.Update(media);
		}
	}
}
