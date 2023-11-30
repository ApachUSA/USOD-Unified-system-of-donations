using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class FundImageController : Controller
	{
		private readonly IFundImageService _fundImageService;

		public FundImageController(IFundImageService fundImageService)
		{
			_fundImageService = fundImageService;
		}

		[HttpPost]
		public async Task<IActionResult> AddImage(Fund_Image fund_Image)
		{
			var response = await _fundImageService.CreateAsync(fund_Image);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("FundDetails", "Fund", new { fund_id = fund_Image.Fund_ID, edit = true });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteImage(int fund_Image_id, int _fund_id)
		{
			var response = await _fundImageService.Delete(fund_Image_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("FundDetails", "Fund", new { fund_id = _fund_id, edit = true });
			}
			throw new Exception(response.Description);
		}
	}
}
