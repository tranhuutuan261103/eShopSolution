using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductApiClient _productApiClient;
        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 1, string keyWord="", int categoryId = 1)
        {
            string languageId = HttpContext.Session.GetString("DefaultLanguage");
            GetManageProductPagingRequest request = new GetManageProductPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Keyword = keyWord,
                LanguageId = languageId,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetProductsPaging(request);
            ViewBag.Keyword = keyWord;
            if (TempData["SuccessMsg"] != null)
            {
                ViewBag.SuccessMsg = TempData["SuccessMsg"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            request.LanguageId = HttpContext.Session.GetString("DefaultLanguage");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productApiClient.Create(request);
            if (result == true)
            {
                TempData["SuccessMsg"] = "Tạo mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Tạo mới sản phẩm thất bại!");
            return View(request);
        }
    }
}
