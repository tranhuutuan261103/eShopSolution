using Microsoft.AspNetCore.Mvc;
using eShopSolution.Application.Catalog.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductImages;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        // https://localhost:port/products?pageIndex=1&pageSize=10&CategoryId=2
        [HttpGet]
        [Route("{languageId}")]
        public async Task<IActionResult> GetPaging(string languageId ,[FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId ,request);
            if (products == null)
                return BadRequest("Cannot find product");
            return Ok(products);
        }

        [HttpGet]
        [Route("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string langguageId)
        {
            var product = await _manageProductService.GetById(productId, langguageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch]
        [Route("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        // Images
        [HttpGet]
        [Route("{productId}/images")]
        public async Task<IActionResult> GetListImages(int productId)
        {
            var image = await _manageProductService.GetListImages(productId);

            if (image == null)
                return BadRequest("Cannot find image");

            return Ok(image);
        }

        [HttpGet]
        [Route("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find image");
            return Ok(image);
        }

        [HttpPost]
        [Route("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm]ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);

            if (imageId == 0)
                return BadRequest();

            var image = await _manageProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut]
        [Route("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isSuccessful = await _manageProductService.UpdateImage(imageId, request);

            if (isSuccessful == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var isSuccessful = await _manageProductService.RemoveImage(imageId);

            if (isSuccessful == 0)
                return BadRequest();

            return Ok();
        }
    }
}
