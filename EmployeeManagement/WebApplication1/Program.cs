using WebApplication1.Services.EmployeeService;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); // For Swagger
            builder.Services.AddSwaggerGen(); // For Swagger
            builder.Services.AddScoped<IEmployeeService, EmployeeService>(); // Correctly placed service registration

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Developer exception page
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Production error handler
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
            app.UseStaticFiles(); // Serve static files

            app.UseRouting(); // Ensure routing works correctly
            app.UseAuthorization(); // Authorization middleware

            app.MapControllers(); // Map endpoints to controllers

            app.Run();
        }
    }
}
