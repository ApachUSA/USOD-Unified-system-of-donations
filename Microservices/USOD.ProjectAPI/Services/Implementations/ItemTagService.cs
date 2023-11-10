using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ItemTagService : IItemTagService
	{
		private readonly IBaseRepository<Item_Tag> _itemTagRepository;

		public ItemTagService(IBaseRepository<Item_Tag> itemTagRepository)
		{
			_itemTagRepository = itemTagRepository;
		}

		public async Task<Item_Tag> CreateAsync(Item_Tag itemTag)
		{
			await _itemTagRepository.Create(itemTag);
			return itemTag;
		}

		public async Task<bool> DeleteAsync(Item_Tag itemTag)
		{
			await _itemTagRepository.Delete(itemTag);
			return true;
		}

		public async Task<List<Item_Tag>> GetAsync()
		{
			return await _itemTagRepository.Get().ToListAsync();
		}

		public async Task<Item_Tag?> GetByIdAsync(int itemTag_id)
		{
			return await _itemTagRepository.Get().FirstOrDefaultAsync(x => x.Item_Tag_ID == itemTag_id);
		}

		public async Task<Item_Tag> UpdateAsync(Item_Tag itemTag)
		{
			await _itemTagRepository.Update(itemTag);
			return itemTag;
		}
	}
}
