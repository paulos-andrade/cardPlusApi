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

            modelBuilder.Entity<Address>()
                  .Ignore(c => c.User);
        }
    }
    public static class Validations
    {
        public static string ValidateHasExistingItemsUsers(User user)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var uservalueMail = context.Users.FirstOrDefault(x => x.Mail == user.Mail || );
            var uservalueCPF = context.Users.FirstOrDefault(x => x.CPF == user.CPF);

            if (uservalueMail != null)
                return "Email já existe na base!";
            else if (uservalueCPF != null) 
                return "CPF já existe na base!";
            else return "";
        }
        public static string ValidateExistingItemsAddresses(Address address)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var uservalue = context.Addresses.FirstOrDefault(x => x.CEP == address.CEP);
            if (uservalue != null)
                return "CEP já existe na base!";
            else return "";
        }
    }

}
