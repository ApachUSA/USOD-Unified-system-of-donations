using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ProjectCardService : IProjectCardService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProjectCardService> _logger;
		private readonly string ApiControllerName = "ProjectCard";

		public ProjectCardService(IHttpClientFactory httpClientFactory, HttpClient httpClient, ILogger<ProjectCardService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Project_Card>> CreateProjectCard(Project_Card projectCard)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", projectCard);
				return await ApiResponse.ProcessApiResponse<Project_Card>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project_Card>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> DeleteProjectCard(int projectCard_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{projectCard_id}");
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Project_Card>> GetByIdAsync(int projectCard_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetById/{projectCard_id}");
				return await ApiResponse.ProcessApiResponse<Project_Card>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project_Card>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Project_Card>> UpdateProjectCard(Project_Card projectCard)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"{ApiControllerName}", projectCard);
				return await ApiResponse.ProcessApiResponse<Project_Card>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project_Card>.Error(ex.Message);
			}
		}
	}
}
