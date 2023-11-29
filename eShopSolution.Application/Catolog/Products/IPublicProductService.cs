using eShopSolution.Application.Catolog.Products.Dtos;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetProductPagingRequest = eShopSolution.Application.Catolog.Products.Dtos.Public.GetProductPagingRequest;

namespace eShopSolution.Application.Catolog.Products
{
    public interface IPublicProductService
    {
        public Task< PageResult<ProductViewModel>> GetAllByCatetoty( GetProductPagingRequest request);
    }
}
