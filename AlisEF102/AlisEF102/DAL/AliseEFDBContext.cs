using System.Data.Entity;
using AlisEF102.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AlisEF102.DAL
{
    public class AliseEFDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<AlisEF102.DAL.AliseEFDBContext>());

        public AliseEFDBContext() : base("name=AliseEFDBContext")
        {
        }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AssetModel> AssetModels { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<AssetCondition> AssetConditions { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<AssetStatus> AssetStatus { get; set; }

        public DbSet<AssignedLocation> AssignedLocations { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<AssignedTo> AssignedToes { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<CheckListItem> CheckListItems { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Asset>()
                .HasMany(c => c.CheckListItems).WithMany(a => a.Assets)
                .Map(t => t.MapLeftKey("AssetID")
                    .MapRightKey("CheckListItemID")
                    .ToTable("AssetCheckListItem"));

            //base.OnModelCreating(modelBuilder);
        }

    }
}
