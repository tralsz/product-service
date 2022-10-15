using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using product_service.config;
using product_service.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace product_service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly Learn_DBContext _DBContext;
        private readonly JwtSettings _jwtSettings;

        public ProductsController(Learn_DBContext dBContext, IOptions<JwtSettings> options)
        {
            _DBContext = dBContext;
            this._jwtSettings = options.Value;
        }


        [HttpGet("getall")]
        public List<productsInfo> Get(string? name, string? description)/*, string description = ""*/
        {

            var allProducts = _DBContext.products.Where(x => (name == null || x.name.Contains(name)) && (description == null || x.Description.Contains(description))).
                Select(x => new productsInfo(x.id,x.name)).ToList();

            return allProducts;
        }

        [HttpGet("getall/{id:int?}")]
        public async Task<IActionResult> getById(int? id)/*, string description = ""*/
        {
            //products productsById = _DBContext.products.Where(x => x.id == id);
            products productsById = await _DBContext.products.FirstOrDefaultAsync(x => x.id == id);
            if(productsById == null)
            {
                return NotFound("Product with Id: " + id + " Not Found");
            }

            return Ok(productsById);
        }
    }
}
