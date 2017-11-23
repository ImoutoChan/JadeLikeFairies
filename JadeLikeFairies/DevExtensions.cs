using System.Threading.Tasks;
using JadeLikeFairies.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JadeLikeFairies
{
    public static class DevExtensions
    {
        public static async Task SeedDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await serviceScope.ServiceProvider.GetService<ISeedService>().Seed();
            }
        }
    }
}