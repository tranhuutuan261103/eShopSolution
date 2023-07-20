﻿using eShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using eShopSolution.Application.Catalog.Common;
using eShopSolution.ViewModels.Catalog.ProductImages;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(EShopDbContext context, IStorageService storageService) 
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                FileSize = request.ImageFile.Length,
                ImagePath = await this.SaveFile(request.ImageFile),
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount ++;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId
                    }
                }
            };

            // Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) throw new EShopExceptions($"Can not find Product: {productId}");

            var images = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach(var image in images)
            {
                _context.Remove(image);
            }
            await _context.SaveChangesAsync();

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            // Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic};

            // Filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

            // Paging
            int totalRow = await query.CountAsync();

            query = query.Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize);

            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount
            }).ToListAsync();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopExceptions($"Can not find Product: {productId}");
            }
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = productTranslation.Name,
                DateCreated = product.DateCreated,
                Description = productTranslation.Description,
                Details = productTranslation.Details,
                LanguageId = productTranslation.LanguageId,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation.SeoAlias,
                SeoDescription = productTranslation.SeoDescription,
                SeoTitle = productTranslation.SeoTitle,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };

            return productViewModel;
        }

        public Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = _context.ProductImages.Find(imageId);
            if (image == null) throw new EShopExceptions($"Can not find Image: {imageId}");
            var viewModel = new ProductImageViewModel()
            {
                Id = image.Id,
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                FilePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return Task.FromResult(viewModel);
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            var listImages = await _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            List<ProductImageViewModel> result = new List<ProductImageViewModel>();
            foreach(var image in listImages)
            {
                result.Add(new ProductImageViewModel()
                {
                    Id = image.Id,
                    FilePath = image.ImagePath,
                    FileSize = image.FileSize,
                    IsDefault = image.IsDefault,
                    Caption = image.Caption,
                    SortOrder = image.SortOrder,
                    DateCreated= image.DateCreated,
                    ProductId = image.ProductId
                });
            }
            return await Task.FromResult(result);
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image != null)
            {
                _context.ProductImages.Remove(image);
            }
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = _context.Products.Find(request.Id);
            var productTranslation = _context.ProductTranslations.FirstOrDefault(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new EShopExceptions($"Can not find Product: {request.Id}");

            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.SeoAlias = request.SeoAlias;

            // Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) 
                throw new EShopExceptions($"Can not find an image with id: {imageId}");
            
            if (request.ImageFile != null) // Update image
            {
                image.FileSize = request.ImageFile.Length;
                image.ImagePath = await this.SaveFile(request.ImageFile);
            }

            _context.ProductImages.Update(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = _context.Products.Find(productId);
            if (product == null) throw new EShopExceptions($"Can not find Product: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null) throw new EShopExceptions($"Can not find Product: {productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
