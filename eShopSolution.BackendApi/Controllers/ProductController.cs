using eShopSolution.Application.Catolog.Products;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    // https://localhost:7080/api/product
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if(product == null)
            {
                return BadRequest("Can't find product");
            }
            return Ok(product);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCatetoty(request);
            return Ok(products);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            if(productId == 0)
            {
                return BadRequest();

            }
            var product = await _manageProductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }




    }
}
