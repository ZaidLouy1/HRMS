using HRMS.DbContexts;
using HRMS.Dtos.Departments;
using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        











        public readonly HRMSContext _dbcontext;
        public DepartmentsController (HRMSContext dbcontext) 
        {
            _dbcontext = dbcontext;


        }













        [HttpGet("GitByCriteria")]

        public IActionResult GitByCriteria([FromQuery] SearchDepartmentDto searchDepartmentDto) // name = Human Resources , floorNumber = 2
        {
            var data = from dep in _dbcontext.Departments
                       where (searchDepartmentDto.name == null || dep.Name.ToUpper().Contains(searchDepartmentDto.name.ToUpper())) && //.ToUpper() :if the user send the string lower case the system will reflect it to upper case to solve the upper and lower case, and contains :if the user write Dev only not Developer The system eill know the object
                       (searchDepartmentDto.FloorNumber == null || dep.FloorNumber == searchDepartmentDto.FloorNumber) 
                       orderby dep.Id descending
                       select new DepartmentDto
                       {
                           Id = dep.Id,
                           Name = dep.Name,
                           Description = dep.Description,
                           FloorNumber=dep.FloorNumber
                       };

            return Ok(data);

        }













        [HttpGet("{id}")]
        public IActionResult GitById(long id)
        {
            var department = _dbcontext.Departments.Select(x => new DepartmentDto
            {
                Id=x.Id,
                Name=x.Name,
                Description=x.Description,
                FloorNumber = x.FloorNumber

            }).FirstOrDefault(x => x.Id == id);

            if (department == null)
            {
                return BadRequest("Department Dose Not Exist");
            }

            return Ok(department);
            }














        [HttpPost]
        public IActionResult Add([FromBody] SaveDepartmentDto newDepartment)
        {
            var department = new Department
            {
                Id = (_dbcontext.Departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = newDepartment.Name,
                Description = newDepartment.Description,
                FloorNumber = newDepartment.FloorNumber
            };

            _dbcontext.Departments.Add(department);
            _dbcontext.SaveChanges();
            return Ok();
        }
















        [HttpPut]
        public IActionResult Update([FromBody] DepartmentDto updateDepartment)
        {
            var department = _dbcontext.Departments.FirstOrDefault(x => x.Id == updateDepartment.Id);
            if (department == null)
            {
                return NotFound("Department Dose Not Exist");
            }
            //department.Id = updateDepartment.Id;
            department.Name = updateDepartment.Name;
            department.Description = updateDepartment.Description;
            department.FloorNumber = updateDepartment.FloorNumber;
            _dbcontext.SaveChanges();
            
            return Ok();
        }














        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var department = _dbcontext.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound("department Not Found");
            }
            _dbcontext.Departments.Remove(department);
            _dbcontext.SaveChanges();
            return Ok();
        }


    }
}
