using Core.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace skynet.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public ProductsController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
           return await storeContext.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await storeContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }
            storeContext.Products.Add(product);
            await storeContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {

            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Cannot update this product");
            }
            storeContext.Entry(product).State = EntityState.Modified;

            await storeContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await storeContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            storeContext.Products.Remove(product);
            await storeContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return storeContext.Products.Any(e => e.Id == id);
        }
    }
}
