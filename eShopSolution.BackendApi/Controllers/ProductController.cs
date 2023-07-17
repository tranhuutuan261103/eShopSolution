﻿using Microsoft.AspNetCore.Mvc;
using eShopSolution.Application.Catalog.Products;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eShopSolution.BackendApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IPublicProductService _publicProductService;
        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }
    }
}
