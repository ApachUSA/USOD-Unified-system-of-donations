using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IItemTagService
	{
		Task<List<Item_Tag>> GetAsync();

		Task<Item_Tag?> GetByIdAsync(int itemTag_id);

		Task<Item_Tag> CreateAsync(Item_Tag itemTag);

		Task<Item_Tag> UpdateAsync(Item_Tag itemTag);

		Task<bool> DeleteAsync(Item_Tag itemTag);
	}
}
