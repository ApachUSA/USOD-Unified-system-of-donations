﻿using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IDonorService
	{
		Task<BaseResponse<string>> Login(Donor_LoginVM donor_LoginVM);

		Task<BaseResponse<List<Donor>>> GetList();

		Task<BaseResponse<Donor>> GetProfile(int id);

		Task<BaseResponse<DonorVM>> GetInfo(int id);

		Task<BaseResponse<List<DonorVM>>> GetInfo(int[] ids);

		Task<BaseResponse<Donor>> Register(Donor donor);

		Task<BaseResponse<Donor>> Update(Donor donor);

		Task<BaseResponse<bool>> Delete(int donor_id);

	}
}
