using ElecrticalShop.Data;
using ElecrticalShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElecrticalShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CountryController(ApplicationDbContext db) 
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            List<Country> objCountry = _db.Countries.ToList();
            if(objCountry == null)
            {
                return NoContent();
            }
            return objCountry;
        }
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult <Country> Create([FromBody]Country country)
        {
            var result = _db.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == country.Name.ToLower().Trim()).Any();
            if(result)
            {
                return Conflict("country already exist in the database");
            }
            _db.Countries.Add(country);
            _db.SaveChanges();
            return CreatedAtAction("GetById", new {id = country.Id},country);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Country> GetById(int id)
        {
            Country objCountry = _db.Countries.FirstOrDefault(c => c.Id == id);
            if(objCountry == null)
            {
                return NoContent();
            }
            return objCountry;
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> Update(int id, [FromBody]Country country)
        {
            if(country == null || id != country.Id)
            {
                return BadRequest();
            }
            var countryFromDb = _db.Countries.FirstOrDefault(x => x.Id == id);
            if(countryFromDb == null)
            {
                return NotFound();
            }
            countryFromDb.Name = country.Name;
            countryFromDb.ShortName = country.ShortName;
            countryFromDb.CountryCode = country.CountryCode;          

            _db.Countries.Update(countryFromDb);
            _db.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country> DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var country = _db.Countries.FirstOrDefault(x => x.Id == id);
            if(country == null)
            {
                return NotFound();
            }
            _db.Countries.Remove(country);
            _db.SaveChanges();
            return Ok();
        }
    }
}
