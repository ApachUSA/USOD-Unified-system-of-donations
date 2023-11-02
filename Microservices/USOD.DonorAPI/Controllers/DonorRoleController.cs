using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;

namespace USOD.DonorAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class DonorRoleController : ControllerBase
	{
		private readonly IBaseRepository<Donor_Role> _donorRoleRepository;

		public DonorRoleController(IBaseRepository<Donor_Role> donorRoleRepository)
		{
			_donorRoleRepository = donorRoleRepository;
		}

		

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var donorRoles = await _donorRoleRepository.Get().ToListAsync();

			if (!donorRoles.Any()) return NotFound();


			return Ok(donorRoles);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var donorRoles = await _donorRoleRepository.Get().FirstOrDefaultAsync(x => x.Donor_Role_ID == id);

			if (donorRoles == null) return NotFound();


			return Ok(donorRoles);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor_Role donorRole)
		{
			try
			{
				await _donorRoleRepository.Create(donorRole);
				return Ok(donorRole);
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}


		}
	}
}
