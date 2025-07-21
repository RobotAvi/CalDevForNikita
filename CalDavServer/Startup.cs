using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Basic;
using Microsoft.AspNetCore.Authentication.Basic.Events;

namespace CalDavServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // TODO: Add authentication, EF Core, and other services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Basic Authentication
            services.AddScoped<AuthService>();
            services.AddScoped<IBasicUserValidationService, BasicUserValidationService>();
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasic<BasicUserValidationService>(options => { });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}