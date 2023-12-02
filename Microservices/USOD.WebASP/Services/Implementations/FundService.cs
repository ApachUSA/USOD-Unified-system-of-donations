using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Fund_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class FundService : IFundService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<FundService> _logger;
		private readonly string ApiControllerName = "Fund";

		public FundService(IHttpClientFactory httpClientFactory, ILogger<FundService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Fund");
			_logger = logger;
		}

		public async Task<BaseResponse<Fund>> CreateFund(Fund fund)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", fund);
				return await ApiResponse.ProcessApiResponse<Fund>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Fund>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> DeleteFund(int fund_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{fund_id}");
				return await ApiResponse.ProcessApiResponse<bool>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Fund>> GetFundByID(int fund_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/{fund_id}");
				return await ApiResponse.ProcessApiResponse<Fund>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Fund>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Fund>>> GetFundByID(int[] fund_ids)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByIds?{string.Join("&", fund_ids.Select(id => $"fund_ids={id}"))}");
				return await ApiResponse.ProcessApiResponse<List<Fund>>(response);
			}
			catch (Exception ex)
			{
				return BaseResponse<List<Fund>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Fund>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Fund>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Fund>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Fund>> UpdateFund(Fund fund)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"{ApiControllerName}", fund);
				return await ApiResponse.ProcessApiResponse<Fund>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Fund>.Error(ex.Message);
			}
		}
	}
}
