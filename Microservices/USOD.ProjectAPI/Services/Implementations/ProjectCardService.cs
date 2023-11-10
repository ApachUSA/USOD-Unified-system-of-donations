using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectCardService : IProjectCardService
	{
		private readonly IBaseRepository<Project_Card> _projectCardRepository;
		private readonly ICardImageService _cardImageService;
		private readonly ICardVideoService _cardVideoService;

		public ProjectCardService(IBaseRepository<Project_Card> projectCardRepository, ICardImageService cardImageService, ICardVideoService cardVideoService)
		{
			_projectCardRepository = projectCardRepository;
			_cardImageService = cardImageService;
			_cardVideoService = cardVideoService;
		}

		public async Task<Project_Card> CreateAsync(Project_Card card)
		{
			await _projectCardRepository.Create(card);
			if(card.Card_Images != null)
			{
				card.Card_Images.ForEach(x => x.Project_Card_ID = card.Project_Card_ID);
				await _cardImageService.CreateAsync(card.Card_Images);
			}

			if (card.Card_Videos != null)
			{
				card.Card_Videos.ForEach(x => x.Project_Card_ID=card.Project_Card_ID);
				await _cardVideoService.CreateAsync(card.Card_Videos);
			}
				
			return card;
		}

		public async Task<List<Project_Card>> CreateAsync(List<Project_Card> cards)
		{
			await _projectCardRepository.Create(cards);
			foreach (var card in cards)
			{
				if (card.Card_Images != null)
				{
					card.Card_Images.ForEach(x => x.Project_Card_ID = card.Project_Card_ID);
					await _cardImageService.CreateAsync(card.Card_Images);
				}

				if (card.Card_Videos != null)
				{
					card.Card_Videos.ForEach(x => x.Project_Card_ID = card.Project_Card_ID);
					await _cardVideoService.CreateAsync(card.Card_Videos);
				}
			}
			return cards;
		}

		public async Task<bool> DeleteAsync(Project_Card card)
		{
			await _projectCardRepository.Delete(card);
			return true;
		}

		public async Task<List<Project_Card>> GetAsync(int project_id)
		{
			return await _projectCardRepository.Get().Where(x => x.Project_ID == project_id).ToListAsync();
		}

		public async Task<Project_Card?> GetByIdAsync(int card_id)
		{
			return await _projectCardRepository.Get().FirstOrDefaultAsync(x => x.Project_Card_ID == card_id);
		}

		public async Task<Project_Card> UpdateAsync(Project_Card card)
		{
			await _projectCardRepository.Update(card);
			return card;
		}
	}
}
