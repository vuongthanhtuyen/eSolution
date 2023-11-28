using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Products.Dtos
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
