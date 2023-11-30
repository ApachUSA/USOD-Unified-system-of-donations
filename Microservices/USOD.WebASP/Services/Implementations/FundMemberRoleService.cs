using Fund_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class FundMemberRoleService : IFundMemberRoleService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<FundMemberRoleService> _logger;
		private readonly string ApiControllerName = "MemberRole";

		public FundMemberRoleService(IHttpClientFactory httpClientFactory, ILogger<FundMemberRoleService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Fund");
			_logger = logger;
		}		

		public async Task<BaseResponse<List<Member_Role>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Member_Role>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Member_Role>>.Error(ex.Message);
			}
		}
	}
}
