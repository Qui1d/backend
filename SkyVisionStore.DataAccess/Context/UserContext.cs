using Microsoft.EntityFrameworkCore;
using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionString);
        }
    }
}