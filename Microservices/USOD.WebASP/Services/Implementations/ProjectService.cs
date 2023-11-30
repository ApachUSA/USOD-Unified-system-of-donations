using Fund_Library.Entity;
using Project_Library.Entity;
using System;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ProjectService : IProjectService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProjectService> _logger;
		private readonly string ApiControllerName = "Project";

		public ProjectService(IHttpClientFactory httpClientFactory, HttpClient httpClient, ILogger<ProjectService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Project>> CreateProject(Project project)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", project);
				return await ApiResponse.ProcessApiResponse<Project>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> DeleteProject(int project_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{project_id}");
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Project>>> GetList()
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}");
				return await ApiResponse.ProcessApiResponse<List<Project>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Project>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Project>> GetPojectByID(int project_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetById/{project_id}");
				return await ApiResponse.ProcessApiResponse<Project>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Project>>> GetPojectByID(int[] project_ids)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/GetByIds?{string.Join("&", project_ids.Select(id => $"ids={id}"))}");
				return await ApiResponse.ProcessApiResponse<List<Project>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Project>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<Project>> UpdateProject(Project project)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"{ApiControllerName}", project);
				return await ApiResponse.ProcessApiResponse<Project>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project>.Error(ex.Message);
			}
		}
	}
}
