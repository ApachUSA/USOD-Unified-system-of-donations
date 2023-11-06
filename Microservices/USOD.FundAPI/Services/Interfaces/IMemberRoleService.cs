using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IMemberRoleService
	{
		Task<List<Member_Role>> GetAsync();

		Task<Member_Role?> GetByIdAsync(int memberRole_id);

		Task<Member_Role> CreateAsync(Member_Role memberRole);

		Task<Member_Role> UpdateAsync(Member_Role memberRole);

		Task<bool> DeleteAsync(Member_Role memberRole);
	}
}
