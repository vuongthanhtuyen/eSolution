using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products
{
    public interface IPublicProductService
    {
        public Task< PageResult<ProductViewModel>> GetAllByCatetoty( GetPublicProductPagingRequest request);
        public Task<List<ProductViewModel>> GetAll();
    }

}
