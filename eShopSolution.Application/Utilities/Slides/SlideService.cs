﻿using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Utilities.Slides;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Utilities.Slides
{
	public class SlideService : ISlideService
	{
		private readonly EShopDbContext _context;
		public SlideService(EShopDbContext context) 
		{
			_context = context;
		}
		public async Task<List<SlideViewModel>> GetAll()
		{
			var slides = await _context.Slides.OrderBy(x => x.SortOrder).Select(x => new SlideViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description,
				Url = x.Url,
				Image = x.Image
			}).ToListAsync();
			return slides;
		}
	}
}
