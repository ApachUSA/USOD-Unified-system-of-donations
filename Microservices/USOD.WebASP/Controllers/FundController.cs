using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.ViewModel;

namespace USOD.WebASP.Controllers
{
	public class FundController : Controller
	{
		private readonly IFundService _fundService;
		private readonly IDonorService _donorService;

		public FundController(IFundService fundService, IDonorService donorService)
		{
			_fundService = fundService;
			_donorService = donorService;
		}

		public async Task<IActionResult> FundIndex()
		{
			var response = await _fundService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				List<FundVM> fundVM = new();
				var tasks = response.Data.Select(async fund =>
				{
					var donors = await _donorService.GetInfo(fund.Fund_Members.Select(x => x.Donor_ID).ToArray());
					fundVM.Add(new FundVM
					{
						Fund = fund,
						Fund_Members = donors.StatusCode == System.Net.HttpStatusCode.OK ? donors.Data : null
					});
				});

				await Task.WhenAll(tasks);
				return View(fundVM);
			}
			throw new Exception(response.Description);
		}

		public async Task<IActionResult> FundDetails(int fund_id)
		{
			var response = await _fundService.GetFundByID(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> FundEdit(int fund_id)
		{
			var response = await _fundService.GetFundByID(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> FundEdit(Fund fund)
		{
			var response = await _fundService.UpdateFund(fund);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("FundEdit", new { fund_id = fund.Fund_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public IActionResult FundCreate() => View();

		[HttpPost]
		public async Task<IActionResult> FundCreate(Fund fund)
		{
			var response = await _fundService.CreateFund(fund);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				//user list
				return RedirectToAction(nameof(FundDetails), new { fund_id = fund.Fund_ID });
			}
			throw new Exception(response.Description);
		}

		public async Task<IActionResult> FundDelete(int fund_id)
		{
			var response = await _fundService.DeleteFund(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction(nameof(FundIndex));
			}
			throw new Exception(response.Description);
		}

	}
}
