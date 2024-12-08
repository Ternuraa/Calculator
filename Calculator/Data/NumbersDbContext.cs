using Calculator.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Data
{
    public class NumbersDbContext : DbContext
    {
        public NumbersDbContext(DbContextOptions<NumbersDbContext> options) : base(options) { }

        public DbSet<NumberRecord> Numbers { get; set; }
    }
}
