using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class PaymentTypeService : IPaymentTypeService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProjectService> _logger;
		private readonly string ApiControllerName = "PaymentType";

		public PaymentTypeService(IHttpClientFactory httpClientFactory, HttpClient httpClient, ILogger<ProjectService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<List<Payment_Type>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Payment_Type>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Payment_Type>>.Error(ex.Message);
			}
		}
	}
}
