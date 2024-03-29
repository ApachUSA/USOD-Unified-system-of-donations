﻿using AutoMapper;
using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class DonorService : IDonorService
	{
		private readonly IDonorMediaService _mediaService;
		private readonly IBaseRepository<Donor> _donorRepository;
		private readonly IMapper _mapper;

		public DonorService(IBaseRepository<Donor> donorRepository, IMapper mapper, IDonorMediaService mediaService)
		{
			_donorRepository = donorRepository;
			_mapper = mapper;
			_mediaService = mediaService;
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
			return await _donorRepository.Get().Include(x => x.Donor_Role).ToListAsync();
		}

		public async Task<Donor?> GetProfileByIDAsync(int donor_id)
		{
			return await _donorRepository.Get()
				.Include(x => x.Donor_Role)
				.Include(x => x.Donor_Medias)
				.ThenInclude(x => x.Media_Type)
				.FirstOrDefaultAsync(x => x.Donor_ID == donor_id);
		}

		public async Task<Donor?> GetByLoginAsync(Donor_LoginVM donor_Login)
		{
			return await _donorRepository.Get()
				.Include(x => x.Donor_Role)
				.FirstOrDefaultAsync(x => x.Login == donor_Login.Login && x.Password == donor_Login.Password);
		}

		public async Task<DonorVM?> GetByIDAsync(int donor_role_id)
		{
			var donor = await _donorRepository.Get()
				.Include(x => x.Donor_Medias)
				.ThenInclude(x => x.Media_Type)
				.FirstOrDefaultAsync(x => x.Donor_ID == donor_role_id);
			if (donor == null) return default;

			var _ = _mapper.Map<DonorVM>(donor);
			_.Media?.ForEach(x => x.Donor = null);
			return _;
		}

		public async Task<List<DonorVM>?> GetByIDAsync(int[] donor_role_ids)
		{
			var donors = await _donorRepository.Get()
				.Include(x => x.Donor_Medias)
				.ThenInclude(x => x.Media_Type)
				.Where(x => donor_role_ids.Contains(x.Donor_ID))
				.ToListAsync();
			if (donors == null) return default;

			var _ = _mapper.Map<List<DonorVM>>(donors);
			_.ForEach(x => x.Media?.ForEach(x => x.Donor = null));
			return _;
		}

		public async Task<Donor> RegisterAsync(Donor donor)
		{
			if (await _donorRepository.Get().FirstOrDefaultAsync(x => x.Username == donor.Username) != null) throw new Exception ("User already exist");
			await _donorRepository.Create(donor);

			List<Donor_Media> medias = new()
			{
				new Donor_Media { Donor_ID = donor.Donor_ID, Donor_Media_Type_ID = 1},
				new Donor_Media { Donor_ID = donor.Donor_ID, Donor_Media_Type_ID = 2},
				new Donor_Media { Donor_ID = donor.Donor_ID, Donor_Media_Type_ID = 3}
			};

			await _mediaService.CreateAsync(medias);

			return donor;
		}

		public async Task<Donor> UpdateAsync(Donor donor)
		{
			await _donorRepository.Update(donor);
			return donor;
		}
	}
}
