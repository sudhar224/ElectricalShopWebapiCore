using ElecrticalShop.Data;
using ElecrticalShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElecrticalShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public ActionResult <IEnumerable<Employee>> GetAll()
        {
            List<Employee> objEmp = _db.Employees.ToList();
            return objEmp;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <Employee> GetById(int id)
        {
            Employee objEmployee = _db.Employees.FirstOrDefault(u => u.Id == id);
            if(objEmployee == null)
            {
                return NotFound();
            }
            return objEmployee;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Employee> Post([FromBody] Employee objEmployee)
        {
            if (objEmployee == null)
            {
                return NoContent();
            }
            _db.Employees.Add(objEmployee);
            _db.SaveChanges();
            return CreatedAtAction("GetById", new { id = objEmployee.Id }, objEmployee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Employee> Update(int id, [FromBody] Employee objEmployee)
        {
            if(id == 0 || id == null)
            {
                return BadRequest();
            }
            Employee empObjFromDb = _db.Employees.FirstOrDefault(u => u.Id ==id);
            if(empObjFromDb == null)
            {
                return NoContent();
            }
            empObjFromDb.EmployeeName = objEmployee.EmployeeName;
            empObjFromDb.Phone = objEmployee.Phone;
            empObjFromDb.Address = objEmployee.Address;
            empObjFromDb.Gender = objEmployee.Gender;
            empObjFromDb.JoinDate = objEmployee.JoinDate;

            _db.Employees.Update(empObjFromDb);
            _db.SaveChanges();
            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <Employee> Delete(int id)
        {
            Employee objEmplyee = _db.Employees.FirstOrDefault(u => u.Id==id);
            if(objEmplyee == null)
            {
                return NoContent();
            }
            _db.Employees.Remove(objEmplyee);
            _db.SaveChanges();
            return Ok();
        }
    }
}
