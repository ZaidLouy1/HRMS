using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Employee
    {
        public long Id { get; set; }// Required

        [MaxLength(50)]
        public string FirstName { get; set; }// Required
        
        [MaxLength(50)]
        public string LastName { get; set; } // Required
        
        [MaxLength(50)]
        public string Email { get; set; }// Required
       
        [MaxLength(50)]
        public string Position {  get; set; }// Required

        public DateTime BirthDate { get; set; }// Required
        
        [MaxLength(50)]
        public string PhoneNumber {  get; set; } // 07, +962 (string the resone = 0,+) // Required

        public bool IsActive { get; set; } // Required

        public DateTime StartDate {  get; set; } // start date in the company // Required

        public DateTime ? EndDate { get; set; }// end date in the company ( (?)=> it mean that it can be nullable ) // (optional)
        public decimal ? Salary { get; set; } // optional






        [ForeignKey("Department")]
        public long ? DepartmentId {  get; set; }

        // Navigation Property
        public Department ? Department { get; set; } // one


        [ForeignKey("Manager")]
        public long ? ManagerId {  get; set; }
        public Employee Manager {  get; set; }// self Navigation Property (one)


        //public ICollection<Employee> Employees {  get; set; } // Navigation Property (Many) 
        
           

    }
}

