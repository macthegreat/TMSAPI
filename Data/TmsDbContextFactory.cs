using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TmsApi.Data;

public class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
{
    public TmsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TmsDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=root");

        return new TmsDbContext(optionsBuilder.Options);
    }
}

