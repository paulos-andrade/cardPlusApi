using Microsoft.EntityFrameworkCore;

namespace cadPlus_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Datasource=cardPlusTI.db; Cache=Shared"); //sobrepõe o método padrão de inicialização do server
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<User>()
         .HasMany(c => c.Addresses)
         .WithOne(e => e.User)
         .IsRequired();
        }
    }

}
