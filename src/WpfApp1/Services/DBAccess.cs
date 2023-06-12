using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class DBAccess : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer();

            base.OnConfiguring(optionsBuilder);
        }
    }
}