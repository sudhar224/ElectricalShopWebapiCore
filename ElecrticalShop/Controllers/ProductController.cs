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
        public ActionResult <Product> Get(int id)
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
            _db.Products.Add(objProduct);
            _db.SaveChanges();
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult <Product> Update(int id, [FromBody] Product productObj)
        {
            _db.Products.Update(productObj);
            _db.SaveChanges();
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult <Product> Delete(int id)
        {
            Product objproduct = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(objproduct);
            _db.SaveChanges();
            return Ok();
        }
    }
}
