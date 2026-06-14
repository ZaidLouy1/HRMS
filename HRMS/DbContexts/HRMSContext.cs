using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DbContexts
{
    public class HRMSContext:DbContext
    {

        public HRMSContext(DbContextOptions<HRMSContext> options): base(options)
        {
            //Options
            // Which Database: Sql Server , Oracle , MySql
            // Connection String: Server Name , Database Name
        }

        // Tables 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments {  get; set; }
    }
}
