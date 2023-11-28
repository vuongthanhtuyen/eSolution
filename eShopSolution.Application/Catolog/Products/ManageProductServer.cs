using eShopSolution.Application.Catolog.Products.Dtos;
using eShopSolution.Application.Dtos;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products
{
    public class ManageProductServer : IManageProductService
    {
        private readonly EShopDbContext _context;
        public ManageProductServer(EShopDbContext context)
        {
            _context = context;
        }
 

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
            };
            _context.Products.Add(product);
            return await   _context.SaveChangesAsync();

        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
