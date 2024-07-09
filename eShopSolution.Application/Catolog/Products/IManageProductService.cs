using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetManageProductPagingRequest = eShopSolution.ViewModels.Catalog.Products.GetManageProductPagingRequest;

namespace eShopSolution.Application.Catolog.Products
{
    public interface IManageProductService
    {
        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int id);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewCount(int productId);        
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> AddImages(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImage(int ProductId);
    }
}
