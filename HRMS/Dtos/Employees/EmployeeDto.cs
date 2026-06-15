namespace HRMS.Dtos.Employees
{
    public class EmployeeDto
    {
         public long Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ? EndDate { get; set; }


        public long ? DepartmentId {  get; set; }
        public string? DepartmentName {  get; set; }
        public long ? ManagerId {  get; set; }
        public string ? ManagerName {  get; set; }
        
    }
}
