using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class MemberRoleService : IMemberRoleService
	{
		private readonly IBaseRepository<Member_Role> _memberRoleRepository;

		public MemberRoleService(IBaseRepository<Member_Role> memberRoleRepository)
		{
			_memberRoleRepository = memberRoleRepository;
		}

		public async Task<Member_Role> CreateAsync(Member_Role memberRole)
		{
			await _memberRoleRepository.Create(memberRole);
			return memberRole;
		}

		public async Task<bool> DeleteAsync(Member_Role memberRole)
		{
			await _memberRoleRepository.Delete(memberRole);
			return true;
		}

		public async Task<List<Member_Role>> GetAsync()
		{
			return await _memberRoleRepository.Get().ToListAsync();
		}

		public async Task<Member_Role?> GetByIdAsync(int memberRole_id)
		{
			return await _memberRoleRepository.Get().FirstOrDefaultAsync(x => x.Member_Role_ID == memberRole_id);
		}

		public async Task<Member_Role> UpdateAsync(Member_Role memberRole)
		{
			await _memberRoleRepository.Update(memberRole);
			return memberRole;
		}
	}
}
