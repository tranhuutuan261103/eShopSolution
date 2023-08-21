using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext eShopDbContext;
        public CategoryService(EShopDbContext eShopDbContext)
        {
            this.eShopDbContext = eShopDbContext;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in eShopDbContext.Categories
                        join ct in eShopDbContext.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            if (query == null)
            {
                return null;
            }

            return await query.Select( x => new CategoryViewModel()
                {
                    Id = x.c.Id,
                    Name = x.ct.Name
                }).ToListAsync();
        }
    }
}
