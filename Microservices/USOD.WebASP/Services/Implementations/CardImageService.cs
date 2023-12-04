using Fund_Library.Entity;
using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class CardImageService : ICardImageService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<CardImageService> _logger;
		private readonly string ApiControllerName = "CardImage";


		public CardImageService(IHttpClientFactory httpClientFactory, ILogger<CardImageService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Card_Image>> CreateAsync(Card_Image cardImage)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", cardImage);
				return await ApiResponse.ProcessApiResponse<Card_Image>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Card_Image>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int cardImage_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{cardImage_id}");
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
