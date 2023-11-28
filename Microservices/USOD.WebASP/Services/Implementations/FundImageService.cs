using Donor_Library.Entity;
using Fund_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class FundImageService : IFundImageService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<FundImageService> _logger;
		private readonly string ApiControllerName = "FundImage";

		public FundImageService(IHttpClientFactory httpClientFactory, ILogger<FundImageService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Fund");
			_logger = logger;
		}

		public async Task<BaseResponse<Fund_Image>> CreateAsync(Fund_Image fundImage)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", fundImage);
				return await ApiResponse.ProcessApiResponse<Fund_Image>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Fund_Image>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int fundImage_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{fundImage_id}");
				return await ApiResponse.ProcessApiResponse<bool>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Fund_Image>>> GetList(int fund_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/{fund_id}");
				return await ApiResponse.ProcessApiResponse<List<Fund_Image>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Fund_Image>>.Error(ex.Message);
			}
		}
	}
}
