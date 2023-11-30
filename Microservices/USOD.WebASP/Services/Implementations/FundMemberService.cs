using Fund_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class FundMemberService : IFundMemberService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<FundMemberService> _logger;
		private readonly string ApiControllerName = "FundMember";


		public FundMemberService(IHttpClientFactory httpClientFactory, ILogger<FundMemberService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Fund");
			_logger = logger;
		}

		public async Task<BaseResponse<Fund_Member>> CreateAsync(Fund_Member fundMember)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", fundMember);
				return await ApiResponse.ProcessApiResponse<Fund_Member>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Fund_Member>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int fundMember_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{fundMember_id}");
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
