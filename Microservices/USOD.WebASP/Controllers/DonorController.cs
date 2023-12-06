using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.Response;

namespace USOD.WebASP.Controllers
{
	public class DonorController : Controller
	{
		private readonly IDonorService _donorService;
		private readonly IMemoryCache _memoryCache;

		public DonorController(IDonorService donorService, IMemoryCache memoryCache)
		{
			_donorService = donorService;
			_memoryCache = memoryCache;
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

		[HttpGet]
		public IActionResult Login() => View();

		[HttpPost]
		public async Task<IActionResult> Login(Donor_LoginVM donor_LoginVM)
		{
			if (ModelState.IsValid)
			{
				var response = await _donorService.Login(donor_LoginVM);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var handler = new JwtSecurityTokenHandler();
					var jsonToken = handler.ReadToken(response.Data) as JwtSecurityToken;

					//var name = jsonToken?.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
					//var role = jsonToken?.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

					//List<Claim> claims = new();

					//if(name != null) claims.Add(new Claim(ClaimTypes.Name, name));
					//if(role != null) claims.Add(new Claim(ClaimTypes.Role, role));

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new(new ClaimsIdentity(jsonToken?.Claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType))
						);

					_memoryCache.Set("Authorization", response.Data, TimeSpan.FromDays(1));
					//Response.Cookies.Append("Authorization", response.Data, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(donor_LoginVM);
		}

		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(Donor donor)
		{
			if (ModelState.IsValid)
			{
				donor.Donor_Role_ID = 1;
				var response = await _donorService.Register(donor);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return RedirectToAction("Login", "Donor", new { donor_LoginVM  = new Donor_LoginVM() {Login = donor.Login, Password = donor.Password } });
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(donor);
		}

		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			var response = await _donorService.GetProfile(int.Parse(HttpContext.User.FindFirst("Id").Value));
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> Profile(Donor donor)
		{
			var response = await _donorService.Update(donor);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("Profile");
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<List<DonorVM>> GetInfo()
		{
			int[] mas = new int[] { 1, 12 };
			var response = await _donorService.GetInfo(mas);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return response.Data;
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

		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			_memoryCache.Remove("Authorization");
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
