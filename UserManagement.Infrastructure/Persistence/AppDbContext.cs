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

        public DbSet<ValuationType> ValuationTypes { get; set; }

        public DbSet<LoadValuation> LoadValuations { get; set; }

        public DbSet<ManualTask> ManualTasks => Set<ManualTask>();

        public DbSet<TaskFields> TaskFields => Set<TaskFields>();

        public DbSet<UserTaskFieldMapping> UserTaskFieldMappings => Set<UserTaskFieldMapping>();

        public DbSet<SellerConfig> sellerConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Sujata", LastName = "Telang", Email = "sujata.telang@infovision.com", Role = "Asset Manager" },
                new User { Id = 2, FirstName = "Kiran", LastName = "Patwardhan", Email = "kiran.patwardhan@infovision.com", Role = "Auction Manager" },
                new User { Id = 3, FirstName = "Sujit", LastName = "Mhetre", Email = "sujit.mhetre@infovision.com", Role = "Marketing Specialist" },
                new User { Id = 4, FirstName = "Tejas", LastName = "Deogadkar", Email = "tejas.deogadkar@infovision.com", Role = "Asset Manager" },
                new User { Id = 5, FirstName = "Dilipkumar", LastName = "Pottigari", Email = "dilipkumar.pottigari@infovision.com", Role = "Asset Manager" }
            );

            modelBuilder.Entity<ManualTask>().HasData(
                new ManualTask { Id = 1, TaskId = 1, TaskName = "MarketingRequestTask" },
                new ManualTask { Id = 2, TaskId = 2, TaskName = "LoadValuationTask" }
            );
            modelBuilder.Entity<TaskFields>().HasData(
                new TaskFields { Id = 1 , TaskId = 1, TaskMappingFields = ["valuationEffectiveDate", "valuationType", "valuationExpires", "inspectionType", "preparedBy", "company", "asIsValue", "repairedValue", "repairedEstimate", "condition", "comments"] },
                new TaskFields { Id = 2, TaskId = 2, TaskMappingFields = ["address1", "address2", "valuationExpires", "city", "state", "zipCode", "county", "apn", "bpoValue", "transactionSubType", "sellerCode", "sellerSubCode", "propertyType", "bedRooms", "fullBathrooms", "partialBathrooms", "emv"] }
            );
            modelBuilder.Entity<UserTaskFieldMapping>().HasData(
                new UserTaskFieldMapping { Id = 1, UserId = 4, TaskId = 1, AccessTaskFields = ["valuationEffectiveDate", "valuationType", "valuationExpires", "inspectionType", "preparedBy", "company", "asIsValue", "repairedValue", "repairedEstimate", "condition", "comments"] },
                new UserTaskFieldMapping { Id = 2, UserId = 4, TaskId = 2, AccessTaskFields = [""] }
            );
        }


    }
}
