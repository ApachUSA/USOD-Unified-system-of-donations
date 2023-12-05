using Donor_Library.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using USOD.WebASP.Models;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}