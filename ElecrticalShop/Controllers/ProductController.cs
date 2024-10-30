using ElecrticalShop.Data;
using ElecrticalShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElecrticalShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <IEnumerable<Product>> Get()
        {
            List<Product> objProduct = _db.Products.ToList();
            if(objProduct == null)
            {
                return NoContent();
            }
            return objProduct;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Product> GetById(int id)
        {
            Product objProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if(objProduct == null)
            {
                return NoContent();
            }
            return objProduct;
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult <Product> Create([FromBody] Product objProduct )
        {
            var result = _db.Products.AsQueryable().Where(u => u.ProductName.ToLower().Trim() == objProduct.ProductName.ToLower().Trim()).Any();
            if(result != null)
            {
                return Conflict("Product Name already exist");
            }
            _db.Products.Add(objProduct);
            _db.SaveChanges();
            return CreatedAtAction("GetById", new {id = objProduct.Id},objProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Product> Update(int id, [FromBody] Product productObj)
        {
            if(productObj == null || id != productObj.Id)
            {
                return BadRequest();
            }
            var productFromdb = _db.Products.FirstOrDefault(x => x.Id == id);
            if(productFromdb == null)
            {
                return NotFound();
            }
            productFromdb.ProductName = productObj.ProductName;
            productFromdb.ProductDescription = productObj.ProductDescription;
            productFromdb.Price = productObj.Price;
            productFromdb.Stock = productObj.Stock;
            _db.Products.Update(productFromdb);
            _db.SaveChanges();
            return NoContent() ;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Product> Delete(int id)
        {
            Product objproduct = _db.Products.FirstOrDefault(x => x.Id == id);
            if(objproduct == null)
            {
                return NoContent();
            }
            _db.Products.Remove(objproduct);
            _db.SaveChanges();
            return Ok();
        }
    }
}
