using HRMS.Dtos.Employees;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    // Data Annotations --> Extra Information
    [Route("api/[controller]")] // [controller] -->Employees 
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Employee Class => Model
        public static List<Employee> employees = new List<Employee>()
        {
            new Employee() {Id=1 ,FirstName ="Ahmad" , LastName = "Nasser" , Email = "Ahmad@123.com" , Position="HR" , BirthDate= new DateTime(1995,1,25) , PhoneNumber="+962799585653",IsActive=true,StartDate=new DateTime(2026,1,1), Salary=600 },
            new Employee() {Id=2 ,FirstName ="Zaid" , LastName = "Almasri" , Email = "zaid@123.com" , Position="Developer" , BirthDate= new DateTime(2001,3,28) , PhoneNumber="+962799585653",IsActive=true,StartDate=new DateTime(2026,1,1), Salary=500 },
            new Employee() {Id=3 ,FirstName ="Yousef" , LastName = "al Faris" , Email = "yousef@123.com" , Position="Manager" , BirthDate= new DateTime(1999,8,17) , PhoneNumber="+962799585653",IsActive=true,StartDate=new DateTime(2026,1,1), Salary=1000 },
            //new Employee() {Id=4 ,FirstName ="Sara" , LastName = "karam" , Email = "sara@123.com" , Position="Developer" , BirthDate= new DateTime(2000,5,2) , PhoneNumber="+962799585653",IsActive=true,StartDate=new DateTime(2026,1,1), Salary=800 }
        };


        //CRUD Operations : Create , Read , Update , Delete
        //Endpoints --> Methods
        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria([FromQuery] string ? position) 
        {
            //Query Syntax
            var data = from emp in employees
                       where (position == null || emp.Position == position) // filteration  // Position == nul || one condition if true return true
                       orderby emp.Id descending
                       select new EmployeeDto // Dto : Data Transfer Object
                       {
                           Id = emp.Id,
                           Name = emp.FirstName + "" + emp.LastName,
                           Position = emp.Position,
                           BirthDate = emp.BirthDate,
                           StartDate = emp.StartDate,
                           EndDate = emp.EndDate
                       };

            return Ok(data);
        }








        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            //var data = employees.FirstOrDefault(x => x.Id == id);
            var data = employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.FirstName + "" + x.LastName,
                Position = x.Position,
                BirthDate = x.BirthDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate

            }).FirstOrDefault(x => x.Id == id);

            if(data == null) 
            {
                return NotFound("Employee Not Found");
            }
            return Ok(data);
        }








        [HttpPost]
        public IActionResult Add([FromBody] SaveEmployeeDto newEmployee)
        {
            var employee = new Employee()
            {
                Id = (employees.LastOrDefault()?.Id ?? 0)+ 1,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Position = newEmployee.Position,
                BirthDate = newEmployee.BirthDate,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate,
                Email = newEmployee.Email,
                IsActive = newEmployee.IsActive,
                PhoneNumber = newEmployee.PhoneNumber,
                Salary = newEmployee.Salary,
            }; 
            employees.Add(employee);

            return Ok(employee.Id);
        }







        [HttpPut]
        public IActionResult Update([FromBody] SaveEmployeeDto updatedEmployee)
        {
            var employee=employees.FirstOrDefault(x=>x.Id == updatedEmployee.Id);
            if (employee == null)
            {
                return NotFound("Employee Dose Not Exist");
            }
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Position = updatedEmployee.Position;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.StartDate = updatedEmployee.StartDate;
            employee.Email = updatedEmployee.Email;
            employee.IsActive = updatedEmployee.IsActive;
            employee.EndDate = updatedEmployee.EndDate;
            employee.Salary = updatedEmployee.Salary;

            return Ok();
        }








        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }
            employees.Remove(employee);
            return Ok();
        }




    }
}

// Query parameter => [FromQuery]
//Request Body => [FromBody]

// simple data type ==> string , int , long ...  ---> (By Default) Query Parameter
// complix data type ==> model , Dto , object ... ---> (By Default) Request Body

// Method Can Use Multiple Parameters of Type [FromQuery]
// Method Can Not Use Multiple Parameters of Type [FromBody]

//Http Post/Put : Can Use Both Body Request [FromBody] And Query Parameter [FromQuery] , But We Will Only Use [FromBody].
//Http Delete : Can Use Both Body Request [FromBody] And Query Parameter [FromQuery] , But We Will Only Use [FromQuery] Or Rout Parameter.
//Http Get : Can not Use Both Body Request [FromBody] , We Can Only Use Query Parameter [FromQuery] Or Rout Parameter with GetById.











//    [HttpGet("GetAll")] // Type of Http Request
//    public IActionResult Get()
//    {
//        return Ok(new { Name = "Ahmad", Age = 25 });// Http Response : Data , 200 OK
//        //return NotFound("No Data Found");// Http Response : Data , 404 Not Found
//        //return BadRequest("Data Not Loaded");// Http Response : Data , 404 Bad Resquest
//        //return StatusCode(500, "An Error Occurred"); // Http Response : Data , 500 Internal Server Error
//    }

//    [HttpPost]
//    public IActionResult GetEmployee()
//    {
//        return Ok();
//    }




