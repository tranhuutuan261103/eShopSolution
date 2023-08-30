using Microsoft.AspNetCore.Mvc;
using eShopSolution.Application.Catalog.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductImages;
using Microsoft.AspNetCore.Authorization;
using eShopSolution.ViewModels.Catalog.Categories;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _ProductService;
        public ProductsController(IProductService manageProductService)
        {
            _ProductService = manageProductService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _ProductService.GetAllPaging(request);
            return Ok(products);
        }

        // https://localhost:port/products?pageIndex=1&pageSize=10&CategoryId=2
        [HttpGet]
        [Route("{languageId}")]
        public async Task<IActionResult> GetPaging(string languageId ,[FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _ProductService.GetAllByCategoryId(languageId ,request);
            if (products == null)
                return BadRequest("Cannot find product");
            return Ok(products);
        }

        [HttpGet]
        [Route("{productId}/{languageId}")]
        // https://localhost:port/api/products/1/vi-VN
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _ProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

		[HttpGet]
		[Route("{languageId}/category/{categoryId}")]
		// https://localhost:port/api/products/1/vi-VN
		public async Task<IActionResult> GetListProductByCategoryId(string languageId, int productId)
		{
			var product = await _ProductService.GetListProductByCategoryId(languageId, productId);
			if (product == null)
				return BadRequest("Cannot find product");
			return Ok(product);
		}

		[HttpGet]
		[Route("featured/{languageId}/{take}")]
		// https://localhost:port/api/products/featured/vi-VN/5
		public async Task<IActionResult> GetFeaturedProducts(string languageId, int take)
		{
			var product = await _ProductService.GetFeaturedProducts(languageId, take);
			if (product == null)
				return BadRequest("Cannot find product");
			return Ok(product);
		}

		[HttpGet]
		[Route("latest/{languageId}/{take}")]
		// https://localhost:port/api/products/latest/vi-VN/5
		public async Task<IActionResult> GetLatestProducts(string languageId, int take)
		{
			var product = await _ProductService.GetLatestProducts(languageId, take);
			if (product == null)
				return BadRequest("Cannot find product");
			return Ok(product);
		}

		[HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _ProductService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _ProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int productId,[FromForm]ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var affectedResult = await _ProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok(true);
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _ProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch]
        [Route("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _ProductService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        // Images
        [HttpGet]
        [Route("{productId}/images")]
        public async Task<IActionResult> GetListImages(int productId)
        {
            var image = await _ProductService.GetListImages(productId);

            if (image == null)
                return BadRequest("Cannot find image");

            return Ok(image);
        }

        [HttpGet]
        [Route("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _ProductService.GetImageById(imageId);
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
            var imageId = await _ProductService.AddImage(productId, request);

            if (imageId == 0)
                return BadRequest();

            var image = await _ProductService.GetImageById(imageId);
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
            var isSuccessful = await _ProductService.UpdateImage(imageId, request);

            if (isSuccessful == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var isSuccessful = await _ProductService.RemoveImage(imageId);

            if (isSuccessful == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPut("{id}/categories")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ProductService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
