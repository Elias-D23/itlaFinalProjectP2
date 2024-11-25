using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Crud.Persistence
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {

        }

        public DbSet<PersonDto> People { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Employee> Employees { get; set; }


        //CREO QUE LO DEBO BORRAR :v
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PersonDto>().HasIndex(p => p.Id).IsUnique();

        }
    }
}
