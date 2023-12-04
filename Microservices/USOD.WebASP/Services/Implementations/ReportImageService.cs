using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Implementations
{
	public class ReportImageService : IReportImageService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ReportImageService> _logger;
		private readonly string ApiControllerName = "ProjectReportImage";


		public ReportImageService(IHttpClientFactory httpClientFactory, ILogger<ReportImageService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient("Project");
			_logger = logger;
		}

		public async Task<BaseResponse<Project_Report_Image>> CreateAsync(Project_Report_Image reportImage)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync($"{ApiControllerName}", reportImage);
				return await ApiResponse.ProcessApiResponse<Project_Report_Image>(response);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BaseResponse<Project_Report_Image>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int reportImage_id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"{ApiControllerName}/{reportImage_id}");
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
