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
        public static List<Department> departments = new List<Department>()
        {
            new Department() {Id = 1, Name="Human Resources",Description="HR Department",FloorNumber=1},
            new Department() {Id = 2, Name="Finance",Description="Finance Department",FloorNumber=2},
            new Department() {Id = 3, Name="Development",Description="development Department",FloorNumber=1}
        };





        [HttpGet("GitByCriteria")]

        public IActionResult GitByCriteria(string ? name , int ? floorNumber) // name = Human Resources , floorNumber = 2
        {
            var data = from dep in departments
                       where (name == null || dep.Name.ToUpper().Contains(name.ToUpper())) && (floorNumber == null || dep.FloorNumber == floorNumber) //.ToUpper() :if the user send the string lower case the system will reflect it to upper case to solve the upper and lower case, and contains :if the user write Dev only not Developer The system eill know the object
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
            var department = departments.Select(x => new DepartmentDto
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
                Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = newDepartment.Name,
                Description = newDepartment.Description,
                FloorNumber = newDepartment.FloorNumber
            };

            departments.Add(department);
            return Ok();
        }






        [HttpPut]
        public IActionResult Update([FromBody] DepartmentDto updateDepartment)
        {
            var department = departments.FirstOrDefault(x => x.Id == updateDepartment.Id);
            if (department == null)
            {
                return NotFound("Department Dose Not Exist");
            }
            //department.Id = updateDepartment.Id;
            department.Name = updateDepartment.Name;
            department.Description = updateDepartment.Description;
            department.FloorNumber = updateDepartment.FloorNumber;
            
            return Ok();
        }






        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var department = departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound("department Not Found");
            }
            departments.Remove(department);
            return Ok();
        }


    }
}
