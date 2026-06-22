using HRMS.DbContexts;
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
       

        // Dependancy Injection
        public readonly HRMSContext _dbContext;
        public EmployeesController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }

         




        //CRUD Operations : Create , Read , Update , Delete
        //Endpoints --> Methods
        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria([FromQuery]SearchEmployeeDto searchEmployeeDto) 
        {
            //Query Syntax
            var data = from emp in _dbContext.Employees
                       from dep in _dbContext.Departments.Where(x=>x.Id==emp.DepartmentId).DefaultIfEmpty()   //left join
                       from man in _dbContext.Employees.Where(x=>x.Id==emp.ManagerId).DefaultIfEmpty()//self and left join
                       //where (searchEmployeeDto.Position == null || emp.Position.Contains(searchEmployeeDto.Position, StringComparison.OrdinalIgnoreCase)) && // filteration  // Position == nul || one condition if true return true
                       where (searchEmployeeDto.Position == null || emp.Position.ToUpper().Contains(searchEmployeeDto.Position.ToUpper())) && // filteration  // Position == nul || one condition if true return true
                       //(searchEmployeeDto.Name == null || emp.FirstName.Contains(searchEmployeeDto.Name,StringComparison.OrdinalIgnoreCase))// filteration
                       (searchEmployeeDto.Name == null || emp.FirstName.ToUpper().Contains(searchEmployeeDto.Name.ToUpper()))// filteration
                       orderby emp.Id descending
                       select new EmployeeDto // Dto : Data Transfer Object
                       {
                           Id = emp.Id,
                           Name = emp.FirstName + "" + emp.LastName,
                           Position = emp.Position,
                           BirthDate = emp.BirthDate,
                           StartDate = emp.StartDate,
                           EndDate = emp.EndDate,
                           DepartmentId = emp.DepartmentId,
                           DepartmentName=dep.Name,
                           ManagerId = emp.ManagerId,
                           ManagerName=man.FirstName
                       };

            return Ok(data);
        }









        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {

            var data = _dbContext.Employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.FirstName + "" + x.LastName,
                Position = x.Position,
                BirthDate = x.BirthDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                DepartmentId = x.DepartmentId,
                DepartmentName=x.Department.Name , // Navigation prop (Department) I Need Only Name from Model (Object) Department
                ManagerId = x.ManagerId,
                ManagerName = x.Manager.FirstName // Navigation prop (Manager) I Need Only FirstName  Model (Object) Manager
            }).FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NotFound("Employee Not Found");
            }
            return Ok(data);
        }


        // one of option to do the joins in Id EndPoint , but it is not acceptable
        /*
         var data = _dbContext.Employees.Join(
              _dbContext.Departments,
              employee => employee.DepartmentId,
              department => department.Id,
              (employee, department) => new EmployeeDto
              {
                  Id = employee.Id,
                  Name = employee.FirstName + " " + employee.LastName,
                  Position = employee.Position,
                  BirthDate = employee.BirthDate,
                  StartDate = employee.StartDate,
                  EndDate = employee.EndDate,
                  DepartmentId = employee.DepartmentId,
                  DepartmentName = department.Name,
              }
          ).FirstOrDefault(x => x.Id == id);
         */









        [HttpPost]
        public IActionResult Add([FromBody] SaveEmployeeDto newEmployee)
        {
            var employee = new Employee()
            {
                // Id = (employees.LastOrDefault()?.Id ?? 0)+ 1,
                Id = 0,// identity in sql server (0) database will change 0 to id number
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
                DepartmentId = newEmployee.DepartmentId,
                ManagerId = newEmployee.ManagerId,
            }; 
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges(); // Go To Database
             
              
            return Ok(employee.Id);
        }








        [HttpPut]
        public IActionResult Update([FromBody] SaveEmployeeDto updatedEmployee)
        {
            var employee=_dbContext.Employees.FirstOrDefault(x=>x.Id == updatedEmployee.Id);
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
            employee.DepartmentId = updatedEmployee.DepartmentId;
            employee.ManagerId = updatedEmployee.ManagerId;
            _dbContext.SaveChanges();


            return Ok();
        }









        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
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




