using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //enable CORS for request originating from http://example1.com and https://example2.com
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://example1.com",
                                        "https://example2.com");
                });
            });

            //add services for controllers
            services.AddControllers();

            //db connection string
            var connection = @"Server=localhost;Database=Test;Trusted_Connection=True;ConnectRetryCount=0";
            //register db context as service using dependency injection
            services.AddDbContext<TestContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //generate HTML error responses
                app.UseDeveloperExceptionPage();
            }

            //Add cors as middleware for cross domain requests
            app.UseCors(MyAllowSpecificOrigins);

            //Add middleware for request rounting
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //Add endpoints for controller actions
                endpoints.MapControllers();
            });
        }
    }
}
