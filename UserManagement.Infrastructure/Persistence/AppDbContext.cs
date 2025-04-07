using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace UserManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<ValuationType> ValuationTypes{ get; set; }

        public DbSet<LoadValuation> LoadValuations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Sujata", LastName = "Telang", Email = "sujata.telang@infovision.com", Role = "Asset Managet" },
                new User { Id = 2, FirstName = "Kiran", LastName = "patwardhan", Email = "kiran.patwardhan@infovision.com", Role = "Auction Manager" },
                new User { Id = 3, FirstName = "Sujit", LastName = "mhetre", Email = "sujit.mhetre@infovision.com", Role = "Marketing Specialist" },
                new User { Id = 4, FirstName = "Tejas", LastName = "deogadkar", Email = "tejas.deogadkar@infovision.com", Role = "Asset Managet" },
                new User { Id = 5, FirstName = "dilipkumar", LastName = "pottigari", Email = "dilipkumar.pottigari@infovision.com", Role = "Asset Managet" }
            );

            modelBuilder.Entity<ManualTask>().HasData(
                new ManualTask { TaskId = 1, TaskName = "MarketingRequestTask" },
                new ManualTask { TaskId = 2, TaskName = "LoadValuationTask" }
            );
            modelBuilder.Entity<TaskFields>().HasData(
                new TaskFields { TaskId = 1, TaskMappingFields = ["valuationEffectiveDate", "valuationType", "valuationExpires", "inspectionType", "preparedBy", "company", "asIsValue", "repairedValue", "repairedEstimate", "condition", "comments"] },
                new TaskFields { TaskId = 2, TaskMappingFields = [""] }
            );
            modelBuilder.Entity<UserTaskFieldMapping>().HasData(
                new UserTaskFieldMapping { UserId = 4, TaskId = 1 , AccessTaskFields = ["valuationEffectiveDate", "valuationType", "valuationExpires", "inspectionType", "preparedBy", "company", "asIsValue", "repairedValue", "repairedEstimate", "condition", "comments"] },
                new UserTaskFieldMapping { UserId = 4, TaskId = 2, AccessTaskFields = [""] }
            );            
        }
    }
}
