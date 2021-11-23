using System.Data.Entity;

namespace GameLibrary
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("Default")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Records> Records { get; set; }
    }
}