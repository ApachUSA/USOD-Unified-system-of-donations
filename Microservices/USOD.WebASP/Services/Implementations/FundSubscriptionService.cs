using Microsoft.AspNetCore.SignalR;
using RealTime_Library;
using System.Xml.Linq;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class FundSubscriptionService : IFundSubscriptionService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<FundSubscriptionService> _logger;
		private readonly string ApiControllerName = "Subscription";


		public FundSubscriptionService(IHttpClientFactory httpClientFactory, ILogger<FundSubscriptionService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("RealTime");
			_logger = logger;
		}

		public async Task<BaseResponse<Subscription>> GetByDonor(Subscription sub)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}/GetByDonor", sub);
				return await ApiResponse.ProcessApiResponse<Subscription>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Subscription>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Subscription>>> GetList(int fund_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByFundId/{fund_id}");
				return await ApiResponse.ProcessApiResponse<List<Subscription>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Subscription>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Subscription>>> GetListByDonor(int donor_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/{donor_id}");
				return await ApiResponse.ProcessApiResponse<List<Subscription>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Subscription>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Subscribe(Subscription sub)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", sub);
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Unsubscribe(int sub_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{sub_id}");
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}
	}
}
