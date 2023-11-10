using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface ICardVideoService
	{
		Task<List<Card_Video>> GetAsync(int card_id);

		Task<Card_Video?> GetByIdAsync(int cardVideo_id);

		Task<Card_Video> CreateAsync(Card_Video cardVideo);

		Task<List<Card_Video>> CreateAsync(List<Card_Video> cardVideos);

		Task<Card_Video> UpdateAsync(Card_Video cardVideo);

		Task<bool> DeleteAsync(Card_Video cardVideo);
	}
}
