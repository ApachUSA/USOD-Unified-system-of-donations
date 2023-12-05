using Project_Library.Entity;
using RealTime_Library;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ProjectCommentService : IProjectCommentService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ProjectCommentService> _logger;
		private readonly string ApiControllerName = "ProjectComment";


		public ProjectCommentService(IHttpClientFactory httpClientFactory, ILogger<ProjectCommentService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("RealTime");
			_logger = logger;
		}

		public async Task<BaseResponse<bool>> CreateComment(Project_Comment comment)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", comment);
				return BaseResponse<bool>.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<List<Project_Comment>>> GetList(int project_id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"{ApiControllerName}/{project_id}");
				return await ApiResponse.ProcessApiResponse<List<Project_Comment>>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<List<Project_Comment>>.Error(ex.Message);
			}
		}
	}
}
