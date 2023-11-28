using eShopSolution.Application.Catolog.Products.Dtos;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products
{
    public interface IPublicProductService
    {
        public PageViewModel<ProductViewModel> GetAllByCatetoty( int categoryId, int pageIndex, int pageSize);
    }
}
