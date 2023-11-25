using Microsoft.AspNetCore.Mvc;

namespace USOD.WebASP.Controllers
{
	public class FundController : Controller
	{
		public IActionResult FundIndex()
		{
			return View();
		}
	}
}
