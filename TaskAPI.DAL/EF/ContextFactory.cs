using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TaskAPI.DAL.EF;

namespace Currency.DAL.EF
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var connectionString = "Server=(local);Database=Persons;Trusted_Connection=True;TrustServerCertificate=true;";
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            return new Context(optionsBuilder.Options);
        }
    }
}
