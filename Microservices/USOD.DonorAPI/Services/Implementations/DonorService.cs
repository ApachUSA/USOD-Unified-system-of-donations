using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class DonorService : IDonorService
	{
		private readonly IBaseRepository<Donor> _donorRepository;

		public DonorService(IBaseRepository<Donor> donorRepository)
		{
			_donorRepository = donorRepository;
		}

		public async Task<bool> CheckUsername(string username)
		{
			return await _donorRepository.Get().AnyAsync(x => x.Username == username);
		}

		public async Task<bool> DeleteAsync(Donor donor)
		{
			await _donorRepository.Delete(donor);
			return true;
		}

		public async Task<List<Donor>> GetAsync()
		{
			return await _donorRepository.Get().ToListAsync();
		}

		public async Task<Donor?> GetByIDAsync(int donor_id)
		{
			return await _donorRepository.Get().FirstOrDefaultAsync(x => x.Donor_ID == donor_id);
		}

		public async Task<Donor?> GetByLoginAsync(Donor_LoginVM donor_Login)
		{
			return await _donorRepository.Get().Include(x => x.Donor_Role).FirstOrDefaultAsync(x => x.Login == donor_Login.Login && x.Password == donor_Login.Password);
		}

		public async Task<Donor> RegisterAsync(Donor donor)
		{
			if (await _donorRepository.Get().FirstOrDefaultAsync(x => x.Username == donor.Username) != null) throw new Exception ("User already exist");
			await _donorRepository.Create(donor);
			return donor;
		}

		public async Task<Donor> UpdateAsync(Donor donor)
		{
			await _donorRepository.Update(donor);
			return donor;
		}
	}
}
