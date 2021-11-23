using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskApp.Models
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // DbContextOptions<ECommerceDbContext>  hangi veri tabanı teknolojisine hangi queryString ile bağlanacağım bilgisi olucak.
            // bu sınıfın nasıl çalışacağını constructor vasıtası ile söylüyoruz.

        }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Customer> Customer { get; set; }
        //public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
