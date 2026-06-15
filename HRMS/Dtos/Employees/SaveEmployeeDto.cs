namespace HRMS.Dtos.Employees
{
    public class SaveEmployeeDto
    {
        public long ? Id { get; set; }// Required
        public string FirstName { get; set; }// Required

        public string LastName { get; set; } // Required

        public string Email { get; set; }// Required

        public string Position { get; set; }// Required

        public DateTime BirthDate { get; set; }// Required

        public string PhoneNumber { get; set; } // 07, +962 (string the resone = 0,+) // Required

        public bool IsActive { get; set; } // Required

        public DateTime StartDate { get; set; } // start date in the company // Required


        public DateTime? EndDate { get; set; }// end date in the company ( (?)=> it mean that it can be nullable ) // (optional)
        public decimal? Salary { get; set; } // optional

        public long ? DepartmentId {  get; set; }
        public long ? ManagerId {  get; set; }
    }
}
