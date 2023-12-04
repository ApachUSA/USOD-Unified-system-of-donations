using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class CardVideoService : ICardVideoService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<CardVideoService> _logger;
		private readonly string ApiControllerName = "CardVideo";


		public CardVideoService(IHttpClientFactory httpClientFactory, ILogger<CardVideoService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Card_Video>> CreateAsync(Card_Video cardVideo)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", cardVideo);
				return await ApiResponse.ProcessApiResponse<Card_Video>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Card_Video>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int cardVideo_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{cardVideo_id}");
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
