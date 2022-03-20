using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodingDays
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Database.DB>(opt => opt.UseMySql(GetConnectionString(), ServerVersion.Parse("10.3.0-mariadb")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(routes => routes.MapControllerRoute("default", "api/{controller}/{action}"));

            Setup(app, env);
        }

        private string GetConnectionString()
        {
            // get config from env
            var host = Environment.GetEnvironmentVariable("MYSQL_HOST");
            var password = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            return $"server={host};port=3306;database=CodingDays;user=root;password={password}";
        }

        private void Setup(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // scoped
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<Database.DB>()!.Database.Migrate();
            }
        }
    }
}
