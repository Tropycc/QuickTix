using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuickTix.Data
{
    public class QuickTixSqlServerFactory : IDesignTimeDbContextFactory<QuickTixContext>
    {
        public QuickTixContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuickTixContext>();

            // Use your SQL Server connection string from secrets/appsettings
            optionsBuilder.UseSqlServer("Server=tcp:nscc-0511519-sql-server.database.windows.net,1433;Initial Catalog=nscc-0511519-sql-database;Persist Security Info=False;User ID=nsccadmin;Password=varqyn-kyJfiv-tydna8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new QuickTixContext(optionsBuilder.Options);
        }
    }
}
