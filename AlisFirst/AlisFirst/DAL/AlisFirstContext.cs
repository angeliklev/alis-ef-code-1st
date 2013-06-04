using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AlisFirst.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AlisFirst.DAL
{
    public class AlisFirstContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<AlisFirst.Models.AlisFirstContext>());

        public DbSet<AlisFirst.Models.Asset> Assets { get; set; }

        public DbSet<AlisFirst.Models.AssetModel> AssetModels { get; set; }

        public DbSet<AlisFirst.Models.AssetCondition> AssetConditions { get; set; }

        public DbSet<AlisFirst.Models.Borrower> Borrowers { get; set; }

        public DbSet<AlisFirst.Models.AssetStatus> AssetStatuses { get; set; }

        public DbSet<AlisFirst.Models.AssignedLocation> AssignedLocations { get; set; }

        public DbSet<AlisFirst.Models.AssignedStatus> AssignedStatus { get; set; }

        public DbSet<AlisFirst.Models.AssignedTo> AssignedToes { get; set; }

        public DbSet<AlisFirst.Models.Category> Categories { get; set; }

        public DbSet<AlisFirst.Models.CheckListItem> CheckListItems { get; set; }

        public DbSet<AlisFirst.Models.Loan> Loans { get; set; }

        public DbSet<AlisFirst.Models.Location> Locations { get; set; }

        public DbSet<AlisFirst.Models.Manufacturer> Manufacturers { get; set; }

        public DbSet<AlisFirst.Models.Repair> Repairs { get; set; }

        public DbSet<AlisFirst.Models.Supplier> Suppliers { get; set; }

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