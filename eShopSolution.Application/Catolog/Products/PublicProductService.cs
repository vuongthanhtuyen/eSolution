﻿using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Catalog.Products.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _context;
        public PublicProductService(EShopDbContext context)
        {
            _context = context;
        }
        public int categoryId { get; set; }


        public async Task<PageResult<ProductViewModel>> GetAllByCatetoty(GetPublicProductPagingRequest request)
        {
            // 1. seclet jonnh
            var quety = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            // 2. filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                quety = quety.Where(p => p.pic.CategoryId == request.CategoryId );
            }
            //3. paging
            int totalRow = await quety.CountAsync();
            var data = await quety.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
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
                    ViewCount = x.p.ViewCount,
                }).ToListAsync();
            // Bỏ qua 10 bảng ghi đầu và bắt đầu lấy

            // 4. select and projectiion
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
