using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JadeLikeFairies.Data
{
    class DesignTimeFairiesContextFactory : IDesignTimeDbContextFactory<FairiesContext>
    {
        public FairiesContext CreateDbContext(string[] args)
        {
            var designConnectionString = "Server=localhost;Port=5432;Database=fairies;UserId=postgres;Password=postgres;";

            var optionsBuilder = new DbContextOptionsBuilder<FairiesContext>();

            optionsBuilder.UseNpgsql(designConnectionString);

            return new FairiesContext(optionsBuilder.Options);
        }
    }
}