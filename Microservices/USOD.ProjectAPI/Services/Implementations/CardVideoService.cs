using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class CardVideoService : ICardVideoService
	{
		private readonly IBaseRepository<Card_Video> _cardVideoRepository;

		public CardVideoService(IBaseRepository<Card_Video> cardVideoRepository)
		{
			_cardVideoRepository = cardVideoRepository;
		}

		public async Task<Card_Video> CreateAsync(Card_Video cardVideo)
		{
			await _cardVideoRepository.Create(cardVideo);
			return cardVideo;
		}

		public async Task<List<Card_Video>> CreateAsync(List<Card_Video> cardVideos)
		{
			await _cardVideoRepository.Create(cardVideos);
			return cardVideos;
		}

		public async Task<bool> DeleteAsync(Card_Video cardVideo)
		{
			await _cardVideoRepository.Delete(cardVideo);
			return true;
		}

		public async Task<List<Card_Video>> GetAsync(int card_id)
		{
			return await _cardVideoRepository.Get().Where(x => x.Project_Card_ID == card_id).ToListAsync();	
		}

		public async Task<Card_Video?> GetByIdAsync(int cardVideo_id)
		{
			return await _cardVideoRepository.Get().FirstOrDefaultAsync(x => x.Card_Video_ID == cardVideo_id);
		}

		public async Task<Card_Video> UpdateAsync(Card_Video cardVideo)
		{
			await _cardVideoRepository.Update(cardVideo);
			return cardVideo;
		}
	}
}
