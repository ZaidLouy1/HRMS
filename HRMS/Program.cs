
using HRMS.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HRMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();// add this package after install
            builder.Services.AddSwaggerGen(); // add this package after install






            // Global Object (HRMSContext)-----------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<HRMSContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("HRMSContext"))
            );
            //----------------------------------------------------------------------------------------------------------------------------------------







            var app = builder.Build(); 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger(); //add this package after install
                app.UseSwaggerUI();//add this package after install

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
