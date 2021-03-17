using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UrlShortener.Core.Abstractions;
using UrlShortener.Core.Services;
using UrlShortener.DataAccess.MongoDb;
using UrlShortener.DataAccess.Repositories;
using UrlShortener.WebApi.Infrastructure;

namespace UrlShortener.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrlShortener.WebApi", Version = "v1" });
            });

            services.AddAutoMapper(cfg => cfg.DisableConstructorMapping(), Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository.MongoRepository<>));
            services.AddOptions<MongoDbSettings>().Bind(Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddTransient<IShortenService, ShortenService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortener.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
