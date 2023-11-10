using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class CardImageService : ICardImageService
	{
		private readonly IBaseRepository<Card_Image> _cardImageRepository;

		public CardImageService(IBaseRepository<Card_Image> cardImageRepository)
		{
			_cardImageRepository = cardImageRepository;
		}

		public async Task<Card_Image> CreateAsync(Card_Image cardImage)
		{
			await _cardImageRepository.Create(cardImage);
			return cardImage;
		}

		public async Task<List<Card_Image>> CreateAsync(List<Card_Image> cardImages)
		{
			await _cardImageRepository.Create(cardImages);
			return cardImages;
		}

		public async Task<bool> DeleteAsync(Card_Image cardImage)
		{
			await _cardImageRepository.Delete(cardImage);
			return true;
		}

		public async Task<List<Card_Image>> GetAsync(int card_id)
		{
			return await _cardImageRepository.Get().Where(x => x.Project_Card_ID == card_id).ToListAsync();
		}

		public async Task<Card_Image?> GetByIdAsync(int cardImage_id)
		{
			return await _cardImageRepository.Get().FirstOrDefaultAsync(x => x.Card_Image_ID == cardImage_id);
		}

		public async Task<Card_Image> UpdateAsync(Card_Image cardImage)
		{
			await _cardImageRepository.Update(cardImage);
			return cardImage;
		}
	}
}
