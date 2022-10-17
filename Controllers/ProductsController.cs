using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using product_service.config;
using product_service.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("edit/{id:int?}")]
        public async Task<IActionResult> editProductById(int? id, [FromBody] products _products)
        {
            products productid = await _DBContext.products.FirstOrDefaultAsync(x => x.id == id);
            if(productid != null)
            {
                productid.name = _products.name;
                productid.Description = _products.Description;
                productid.ImageUrl = _products.ImageUrl;
                try
                {
                    _DBContext.SaveChanges();
                }
                catch(DbUpdateException exc)
                {
                    if (exc.InnerException.Message.ToLower().Contains("duplicate"))
                    {
                        return StatusCode(500, "Product with the name " + _products.name + " Already Exist");
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
            }
            else { 
                return NotFound("Product with Id: " + id + " Not Found"); 
            }
            
            return Ok("Product Updated");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewProduct([FromBody] products _products)
        {
            var x = _DBContext.products.Add(_products);
            try
            {
                await _DBContext.SaveChangesAsync();
            }
            catch (DbUpdateException exc)
            {
                if (exc.InnerException.Message.ToLower().Contains("duplicate")){
                    return StatusCode(500, "Product with the name " + _products.name + " Already Exist");
                }
                else
                {
                    return StatusCode(500);
                }
                
            }
            
            return Ok("Product Saved with id " + _products.id  );
        }

        //[HttpDelete("delete/{id:int}")]
        //public async Task<IActionResult> deleteProductById(int id)
        //{
        //    try
        //    {
        //        _DBContext.Remove(new products() { id = id });
        //        await _DBContext.SaveChangesAsync();
        //    }catch(Exception ex)
        //    {
        //        if (!_DBContext.products.Any(x => x.id == id))
        //        {
        //            return NotFound("Product with Id " + id + " Not Found");
        //        }
        //        else
        //        {
        //            throw ex;
        //        }
        //    }
        //    return Ok("Product Deleted");
            
        //}

    }
}
