using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing;
using System.Security.Claims;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Controllers
{
	public class DonorController : Controller
	{
		private readonly IDonorService _donorService;

		public DonorController(IDonorService donorService)
		{
			_donorService = donorService;
		}

		[HttpGet]
		public async Task<IActionResult> DonorList()
		{
			var response = await _donorService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		//[HttpGet]
		//public IActionResult Login() => View();

		[HttpGet]
		public async Task<IActionResult> Login()
		{
			if (ModelState.IsValid)
			{
				var response = await _donorService.Login(new Donor_LoginVM() {  Login = "admin", Password = "admin"});
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					List<Claim> claims = new()
					{
						new Claim(ClaimTypes.Name, "admin")
					};
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new( new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)));
					Response.Cookies.Append("Authorization", response.Data);
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View();
		}

		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(Donor donor)
		{
			var response = await _donorService.Register(donor);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> Profile(int id)
		{
			var response = await _donorService.GetById(id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProfile(Donor donor)
		{
			var response = await _donorService.Update(donor);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int donor_id)
		{
			var response = await _donorService.Delete(donor_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}
	}
}
