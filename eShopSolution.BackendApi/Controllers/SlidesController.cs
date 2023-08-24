using eShopSolution.Application.Utilities.Slides;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
	[Route("api/slides")]
	[ApiController]
	public class SlidesController : Controller
	{
		private readonly ISlideService slideService;
		public SlidesController(ISlideService slideService)
		{
			this.slideService = slideService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var slides = await slideService.GetAll();
			return Ok(slides);
		}
	}
}
