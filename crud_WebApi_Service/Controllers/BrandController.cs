using Crud_with_webApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_with_webApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public BrandController(MyAppDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Brand>> GetAll()
        {

            var products = _context.brands.ToList();
            if (products.Any())
            {
                return Ok(products);
            }
            return BadRequest("Products not found");
        }

        [HttpPost("Addnew")]
        public ActionResult<IEnumerable<Brand>> Addnew(Brand brand)
        {
            if (brand.id == 0) {
                _context.brands.Add(brand);
                _context.SaveChanges();
                return Ok("Prodcut saved successfully");
            }
            else
            {
                return BadRequest("You can not enter id ");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Brand> Edit(int id)
        {
            var product = _context.brands.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut]
        public ActionResult<Brand> Update(int id, Brand brand)
        {
            var product = _context.brands.Find(brand.id);
            if (product == null)
            {
                return BadRequest("id not found");
            }
            else
            {
                product.price = brand.price;
                product.description = brand.description;
                product.quantity = brand.quantity;
                _context.brands.Update(product);
                _context.SaveChanges();
            }
            return Ok("products updated successfully");
        }
        [HttpDelete("{id}")]
        public ActionResult<Brand> Delete(int id)
        {
            var product = _context.brands.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.brands.Remove(product);
            _context.SaveChanges();
            return Ok("Prodcut deleted succesfully");
        }


    }
}
