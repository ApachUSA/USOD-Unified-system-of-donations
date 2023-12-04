using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ItemTagService : IItemTagService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ItemTagService> _logger;
		private readonly string ApiControllerName = "ItemTag";

		public ItemTagService(IHttpClientFactory httpClientFactory, HttpClient httpClient, ILogger<ItemTagService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<List<Item_Tag>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Item_Tag>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Item_Tag>>.Error(ex.Message);
			}
		}
	}
}
