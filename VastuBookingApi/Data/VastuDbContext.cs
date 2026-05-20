using Microsoft.EntityFrameworkCore;
using VastuBookingApi.Models;

namespace VastuBookingApi.Data
{
    public class VastuDbContext : DbContext
    {
        public VastuDbContext(DbContextOptions<VastuDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}