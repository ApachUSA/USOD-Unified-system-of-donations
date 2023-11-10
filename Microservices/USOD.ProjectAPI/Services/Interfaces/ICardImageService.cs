using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface ICardImageService
	{
		Task<List<Card_Image>> GetAsync(int card_id);

		Task<Card_Image?> GetByIdAsync(int cardImage_id);

		Task<Card_Image> CreateAsync(Card_Image cardImage);

		Task<List<Card_Image>> CreateAsync(List<Card_Image> cardImages);

		Task<Card_Image> UpdateAsync(Card_Image cardImage);

		Task<bool> DeleteAsync(Card_Image cardImage);
	}
}
