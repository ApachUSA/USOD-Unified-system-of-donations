using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class DonorService : IDonorService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<DonorService> _logger;
		private readonly string ApiControllerName = "Donor";

		public DonorService(IHttpClientFactory httpClientFactory, ILogger<DonorService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Donor");
			_logger = logger;
		}

		public async Task<BaseResponse<Donor>> GetProfile(int id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetProfileById/{id}");
				return await ApiResponse.ProcessApiResponse<Donor>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<Donor>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Donor>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Donor>>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<List<Donor>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<DonorVM>> GetInfo(int id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetById/{id}");
				return await ApiResponse.ProcessApiResponse<DonorVM>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<DonorVM>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<DonorVM>>> GetInfo(int[] ids)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByIds?{string.Join("&", ids.Select(id => $"ids={id}"))}");
				return await ApiResponse.ProcessApiResponse<List<DonorVM>>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<List<DonorVM>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<string>> Login(Donor_LoginVM donor_LoginVM)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}/Login", donor_LoginVM);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var value = await response.Content.ReadAsStringAsync();
					return BaseResponse<string>.Success(value);
				}
				else return BaseResponse<string>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				return BaseResponse<string>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Donor>> Register(Donor donor)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", donor);
				return await ApiResponse.ProcessApiResponse<Donor>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<Donor>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Donor>> Update(Donor donor)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"{ApiControllerName}", donor);
				return await ApiResponse.ProcessApiResponse<Donor>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<Donor>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int donor_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{donor_id}");
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return BaseResponse<bool>.Success(true);
				}
				else return BaseResponse<bool>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync());
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Error(ex.Message);
			}
		}


	}
}
