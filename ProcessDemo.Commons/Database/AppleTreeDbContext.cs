using Microsoft.EntityFrameworkCore;

namespace ProcessDemo.Commons.Database
{
    public class AppleTreeDbContext : DbContext
    {
            //Connection string to a local database on your machine
            const string connectionString = @"Data Source =(LocalDb)\MSSQLLocalDB; Initial Catalog=ProcessDemo;MultipleActiveResultSets=true;Integrated Security=True;";


            public AppleTreeDbContext() : base()
            {
            }

            public AppleTreeDbContext(DbContextOptions<AppleTreeDbContext> options) : base(options)
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                optionsBuilder.UseSqlServer(connectionString);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            }

            //Our database will have one table called AppleTrees
            public DbSet<AppleTree> AppleTrees { get; set; }


      
    }
}
