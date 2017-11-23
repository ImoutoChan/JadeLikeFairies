using AutoMapper;
using JadeLikeFairies.Data;
using JadeLikeFairies.Services;
using JadeLikeFairies.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JadeLikeFairies
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
            // configuration

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("Configuration"));
            services.AddTransient<AppSettings>(servs => servs.GetService<IOptions<AppSettings>>().Value);

            // services

            services.AddDbContext<FairiesContext>((serviceProvider, optionBuilder) =>
            {
                var connectionString = serviceProvider.GetService<AppSettings>().ConnectionString;
                optionBuilder.UseNpgsql(connectionString);
            }, ServiceLifetime.Scoped);

            services.AddAutoMapper(expression => expression.AddProfile(typeof(ServicesAutoMapperProfile)));

            services.AddScoped<INovelsService, NovelsService>();
            services.AddTransient<ISeedService, SeedService>();

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ISeedService>().Seed().Wait();
            }
        }
    }
}
