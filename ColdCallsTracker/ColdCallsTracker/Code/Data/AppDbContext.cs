using ColdCallsTracker.Code.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ColdCallsTracker.Code.Data
{
    public class AppDbContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<State> States { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CallRecord> CallRecords { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Costing> Costings { get; set; }
        public DbSet<CostingTemplate> CostingTemplates { get; set; }
    }
}
