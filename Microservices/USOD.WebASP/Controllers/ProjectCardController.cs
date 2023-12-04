using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class ProjectCardController : Controller
	{
		private readonly IProjectCardService _projectCardService;
		private readonly IItemTagService _itemTagService;
		private readonly ICardImageService _cardImageService;
		private readonly ICardVideoService _cardVideoService;

		public ProjectCardController(IProjectCardService projectCardService, IItemTagService itemTagService, ICardImageService cardImageService, ICardVideoService cardVideoService)
		{
			_projectCardService = projectCardService;
			_itemTagService = itemTagService;
			_cardImageService = cardImageService;
			_cardVideoService = cardVideoService;
		}

		[HttpGet]
		public async Task<IActionResult> CardCreate(int project_id)
		{
			var response = await _itemTagService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["ItemTags"] = new SelectList(response.Data.OrderBy(x => x.Item_Tag_Name), "Item_Tag_ID", "Item_Tag_Name");
				ViewData["ProjectId"] = project_id;
				return View();
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> CardCreate(Project_Card projectCard)
		{
			var response = await _projectCardService.CreateProjectCard(projectCard);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("CardEdit", new { projectCard_id = response.Data.Project_Card_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> CardEdit(int projectCard_id)
		{
			var response = await _projectCardService.GetByIdAsync(projectCard_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var tag_list = await _itemTagService.GetList();
				if(tag_list.StatusCode == System.Net.HttpStatusCode.OK)
					ViewData["ItemTags"] = new SelectList(tag_list.Data.OrderBy(x => x.Item_Tag_Name), "Item_Tag_ID", "Item_Tag_Name");
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> CardEdit(Project_Card projectCard)
		{
			var response = await _projectCardService.UpdateProjectCard(projectCard);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", "Project", new { project_id = projectCard.Project_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> CardDelete(int projectCard_id, int project_id)
		{
			var response = await _projectCardService.DeleteProjectCard(projectCard_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", "Project", new { project_id = project_id });
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> AddCardImage(Card_Image card_Image)
		{
			var response = await _cardImageService.CreateAsync(card_Image);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("CardEdit", new { projectCard_id = card_Image.Project_Card_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteCardImage(int cardImage_id, int card_id)
		{
			var response = await _cardImageService.Delete(cardImage_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("CardEdit", new { projectCard_id = card_id });
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> AddCardVideo(Card_Video card_Video)
		{
			var response = await _cardVideoService.CreateAsync(card_Video);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("CardEdit", new { projectCard_id = card_Video.Project_Card_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteCardVideo(int cardVideo_id, int card_id)
		{
			var response = await _cardVideoService.Delete(cardVideo_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("CardEdit", new { projectCard_id = card_id });
			}
			throw new Exception(response.Description);
		}
	}
}
