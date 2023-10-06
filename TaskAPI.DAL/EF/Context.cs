using Microsoft.EntityFrameworkCore;
using TaskAPI.Entities;

namespace TaskAPI.DAL.EF
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            
            Database.EnsureCreated();
        }
    }
}
