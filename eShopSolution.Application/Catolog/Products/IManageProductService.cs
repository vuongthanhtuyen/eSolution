using eShopSolution.Application.Catolog.Products.Dtos;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int id);

        Task<List <ProductViewModel>> GetAll();

        Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);



    }
}
