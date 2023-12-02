using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ProjectFundService : IProjectFundService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProjectService> _logger;
		private readonly string ApiControllerName = "ProjectFund";

		public ProjectFundService(IHttpClientFactory httpClientFactory, HttpClient httpClient, ILogger<ProjectService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Project_Fund>> CreateProjectFund(Project_Fund projectFund)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", projectFund);
				return await ApiResponse.ProcessApiResponse<Project_Fund>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project_Fund>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> DeleteProjectFund(int projectFund_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{projectFund_id}");
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Project_Fund>>> GetByFundIdAsync(int fund_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByFundId/{fund_id}");
				return await ApiResponse.ProcessApiResponse<List<Project_Fund>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Project_Fund>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Project_Fund>>> GetByProjectIdAsync(int project_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByProjectId/{project_id}");
				return await ApiResponse.ProcessApiResponse<List<Project_Fund>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Project_Fund>>.Error(ex.Message);
			}
		}
	}
}
