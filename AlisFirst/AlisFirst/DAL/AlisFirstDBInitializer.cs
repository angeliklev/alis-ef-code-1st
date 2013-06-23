using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AlisFirst.Models;

namespace AlisFirst.DAL
{
    public class AlisFirstDBInitializer : DropCreateDatabaseAlways<AlisFirstContext>
    {
        
        protected override void Seed(AlisFirstContext context)
        {
            var manufacturer = new List<Manufacturer>
            {
                new Manufacturer { ManufacturerID = 1, ManufacturerName = "Apple" },
                new Manufacturer { ManufacturerID = 2, ManufacturerName = "Canon" },
                new Manufacturer { ManufacturerID = 3, ManufacturerName = "Dell" }, 
                new Manufacturer { ManufacturerID = 4, ManufacturerName = "HP" },
                new Manufacturer { ManufacturerID = 5, ManufacturerName = "JVC" },
                new Manufacturer { ManufacturerID = 6, ManufacturerName = "Panasonic" },
                new Manufacturer { ManufacturerID = 7, ManufacturerName = "SONY" }               
            };
            manufacturer.ForEach(m => context.Manufacturers.Add(m));
            context.SaveChanges();

            var category = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Laptop" },
                new Category { CategoryID = 2, CategoryName = "Desktop" },
                new Category { CategoryID = 3, CategoryName = "Tablet" },
                new Category { CategoryID = 4, CategoryName = "Digital Video Camera" },
                new Category { CategoryID = 5, CategoryName = "Digital Still Camera" }
            };
            category.ForEach(c => context.Categories.Add(c));
            var i = context.SaveChanges();

            var assetmodel = new List<AssetModel>
            {
                new AssetModel { AssetModelID = 1, AssetModelName = "Elitebook 8470p",  ManufacturerID = 4 },
                new AssetModel { AssetModelID = 2, AssetModelName = "Precision T1600",  ManufacturerID = 3 },
                new AssetModel { AssetModelID = 3, AssetModelName = "iPad 2 16GB WIFI", ManufacturerID = 1 },
                new AssetModel { AssetModelID = 4, AssetModelName = "Precision T1500",  ManufacturerID = 3 },
                new AssetModel { AssetModelID = 5, AssetModelName = "MV920",            ManufacturerID = 2 },
                new AssetModel { AssetModelID = 6, AssetModelName = "GZ-HD7",           ManufacturerID = 5 },
                new AssetModel { AssetModelID = 7, AssetModelName = "GZ-MG135",         ManufacturerID = 5 },
                new AssetModel { AssetModelID = 8, AssetModelName = "Mini DV MV430i 200X Z0", ManufacturerID = 2 },
                new AssetModel { AssetModelID = 9, AssetModelName = "NV-GN250GN 3CCD",   ManufacturerID = 6 },
                new AssetModel { AssetModelID = 10, AssetModelName = "HC85E",            ManufacturerID = 7 },
                new AssetModel { AssetModelID = 11, AssetModelName = "DSR-200AP",        ManufacturerID = 7 },
                new AssetModel { AssetModelID = 12, AssetModelName = "Digital Ixus 60",  ManufacturerID = 2 },
                new AssetModel { AssetModelID = 13, AssetModelName = "Ixus 70",          ManufacturerID = 2 },
                new AssetModel { AssetModelID = 14, AssetModelName = "Ixus 75",          ManufacturerID = 2 },
                new AssetModel { AssetModelID = 15, AssetModelName = "Ixus 80",          ManufacturerID = 2 },
                new AssetModel { AssetModelID = 16, AssetModelName = " PowerShot A310",  ManufacturerID = 2 }
            };
            assetmodel.ForEach(a => context.AssetModels.Add(a));
            i = context.SaveChanges();

            var supplier = new List<Supplier>
            {
                new Supplier { SupplierID = 1, SupplierName = "Winthrop Australia" },
                new Supplier { SupplierID = 2, SupplierName = "Dell" },
                new Supplier { SupplierID = 3, SupplierName = "Harris Technology" },
                new Supplier { SupplierID = 4, SupplierName = "HP" }
            };
            supplier.ForEach(s => context.Suppliers.Add(s));
            i = context.SaveChanges();

            var locations = new List<Location>
            {
                new Location { LocationID = 1, LocationName = "681.G-08" },
                new Location { LocationID = 2, LocationName = "681.4-07" },
                new Location { LocationID = 3, LocationName = "681.2-17" },
                new Location { LocationID = 4, LocationName = "681.G-01" },
                new Location { LocationID = 5, LocationName = "681.G-24" },
                new Location { LocationID = 6, LocationName = "681.4-08" },
                new Location { LocationID = 7, LocationName = "681.4-04" },
                new Location { LocationID = 8, LocationName = "681.4-05" }
            };
            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var assetStatuses = new List<AssetStatus>
            {
                new AssetStatus { AssetStatusID = 1, AssetStatusName = "In Use"},
                new AssetStatus { AssetStatusID = 2, AssetStatusName = "Out of Order"},
                new AssetStatus { AssetStatusID = 3, AssetStatusName = "Return on Warranty"},
                new AssetStatus { AssetStatusID = 4, AssetStatusName = "Maintenance"},
                new AssetStatus { AssetStatusID = 5, AssetStatusName = "Missing"},
                new AssetStatus { AssetStatusID = 6, AssetStatusName = "In Storage"},
                new AssetStatus { AssetStatusID = 7, AssetStatusName = "Written Off"}
            };
            assetStatuses.ForEach(a => context.AssetStatuses.Add(a));
            context.SaveChanges();

            var asset = new List<Asset>
            {
                new Asset 
                {
                    BarCode = "ALVA-0001",
                    SerialNum = "CNU3089V53",
                    DateRecieved = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    PurchaseCost = 1950m,
                    WarrantyExpires =  DateTime.ParseExact("28022016", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    IsLoanable = false,
                    SupplierID = 4,
                    CategoryID = 1,
                    AssetModelID = 1
                },
                new Asset 
                {
                    BarCode = "ALVA-0002",
                    SerialNum = "CNU3089V58",
                    DateRecieved = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    PurchaseCost = 1950m,
                    WarrantyExpires =  DateTime.ParseExact("28022016", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    IsLoanable = false,
                    SupplierID = 4,
                    CategoryID = 1,
                    AssetModelID = 1
                },
                new Asset 
                {
                    BarCode = "ALVA-0004",
                    SerialNum = "7SXQB2S",
                    DateRecieved = DateTime.ParseExact("02122011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    PurchaseCost = 1845m,
                    WarrantyExpires = DateTime.ParseExact("02122014", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    IsLoanable = false,
                    Notes = "I am real bits, not merely an object",
                    SupplierID = 2,
                    CategoryID = 2,
                    AssetModelID = 2
                },
                new Asset 
                {
                    BarCode = "ALVA-0006",
                    SerialNum = "DLXG1ZYCDKPH",
                    DateRecieved = DateTime.ParseExact("03082011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    PurchaseCost = 1845m,
                    WarrantyExpires = DateTime.ParseExact("03082012", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    IsLoanable = false,
                    Notes = "I am real bits, not merely an object",
                    SupplierID = 1,
                    CategoryID = 3,
                    AssetModelID = 3
                },
                new Asset 
                {
                    BarCode = "ALVA-0014",
                    SerialNum = "4SXY72S",
                    DateRecieved = DateTime.ParseExact("23012011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    PurchaseCost = 1845m,
                    WarrantyExpires = DateTime.ParseExact("23012014", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    IsLoanable = false,
                    Notes = "I am real bits, not merely an object",
                    SupplierID = 2,
                    CategoryID = 2,
                    AssetModelID = 4
                },
                new Asset 
                {
                    BarCode = "ALVA-0023", SerialNum = "504262101728", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 5
                },
                new Asset 
                {
                    BarCode = "ALVA-0024", SerialNum = "504262122638", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 5
                },
                new Asset 
                {
                    BarCode = "ALVA-0027", SerialNum = "11250691", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0028", SerialNum = "9251112", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0029", SerialNum = "9251474", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0030", SerialNum = "112G1880", IsLoanable = true,
                    SupplierID = 3, CategoryID = 4, AssetModelID = 7
                }
            }; 
            asset.ForEach(a => context.Assets.Add(a));
            context.SaveChanges();
             
            var checkListItems = new List<CheckListItem>
            {
                new CheckListItem { CheckListItemName = "Leather bag", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Recharger", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Memory stick", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Power adapter", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Flash Unit", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Remote Control", Assets = new List<Asset>()},
                new CheckListItem { CheckListItemName = "Lenses", Assets = new List<Asset>()},
            };
            checkListItems.ForEach(c => context.CheckListItems.Add(c));
            context.SaveChanges();

            checkListItems[0].Assets.Add(asset[5]);
            checkListItems[0].Assets.Add(asset[6]);
            checkListItems[0].Assets.Add(asset[7]);
            checkListItems[1].Assets.Add(asset[8]);
            checkListItems[1].Assets.Add(asset[7]);
            checkListItems[3].Assets.Add(asset[6]);
            checkListItems[3].Assets.Add(asset[7]);
            context.SaveChanges();

            var assignedLocations = new List<AssignedLocation>
            {
                new AssignedLocation { AssetID = 1, LocationID = 1, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 2, LocationID = 3, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 3, LocationID = 2, AssignedLocationDate = DateTime.ParseExact("12012011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 4, LocationID = 2, AssignedLocationDate = DateTime.ParseExact("08022011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 5, LocationID = 4, AssignedLocationDate = DateTime.ParseExact("23012011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 6, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 7, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 8, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 9, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 10, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedLocation { AssetID = 11, LocationID = 5, AssignedLocationDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)}
            };
            assignedLocations.ForEach(a => context.AssignedLocations.Add(a));
            context.SaveChanges();

            var assignedStatuses = new List<AssignedStatus>
            {
                new AssignedStatus {AssetID = 1, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture) },
                new AssignedStatus {AssetID = 2, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture) },
                new AssignedStatus {AssetID = 3, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("12012011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 4, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("08022011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 5, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("23012011", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 5, AssetStatusID = 2, AssignedDate = DateTime.ParseExact("28022012", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 5, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("26032012", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 6, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 7, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 8, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 9, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 10, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 11, AssetStatusID = 1, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
                new AssignedStatus {AssetID = 11, AssetStatusID = 6, AssignedDate = DateTime.ParseExact("25042013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)}
            };
            assignedStatuses.ForEach(a => context.AssignedStatus.Add(a));
            context.SaveChanges();

            var conditions = new List<AssetCondition>
            {
                new AssetCondition { AssetID = 5, Description = "5sm scratch on the screen.", IssuedDate =  DateTime.ParseExact("28022012", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)}
            };
            conditions.ForEach(c => context.AssetConditions.Add(c));
            context.SaveChanges();

            var repairs = new List<Repair> 
            {
                new Repair { AssetID =5, Result = "Motherboard replaced - AUD230.", TechnicianName = "Ben White", IssuedDate =  DateTime.ParseExact("26032012", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)}
            };
            repairs.ForEach(r => context.Repairs.Add(r));
            context.SaveChanges();

            var borrower = new List<Borrower>
            {
                new Borrower 
                {
                    
                    BorrowerID = 1,
                    BarCode = "A1B2C3D4E5F6",
                    GivenName = "Jonathan",
                    Surname = "Johnson",
                    BorrowerExpiryDate = DateTime.ParseExact("06042016", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    Email = "jj@gmail.com",
                    PhoneNumber = "0893336555",
                    IsEmployee = true
                },
                new Borrower 
                {
                    BorrowerID = 2,
                    BarCode = "Z26Y25X24",
                    GivenName = "Vladmir",
                    Surname = "Blaskov",
                    BorrowerExpiryDate = DateTime.ParseExact("11062014", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    Email = "vlad.b@gmail.com",
                    PhoneNumber = "0898765432",
                    IsEmployee = false
                },
                new Borrower 
                {
                    BorrowerID = 3,
                    BarCode = "1010101010101",
                    GivenName = "Popo",
                    Surname = "Lak",
                    BorrowerExpiryDate = DateTime.ParseExact("06042016", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture),
                    Email = "popolak@apple.com",
                    PhoneNumber = "0893336555",
                    IsEmployee = false
                }
            };
            borrower.ForEach(b => context.Borrowers.Add(b));
            context.SaveChanges();

            var assignedTo = new List<AssignedTo>
            {
                new AssignedTo { AssetID = 1, BorrowerID = 2, AssignedDate = DateTime.ParseExact("28022013", "ddMMyyyy", System.Globalization.CultureInfo.CurrentCulture)},
            };
            assignedTo.ForEach(a => context.AssignedToes.Add(a));
            context.SaveChanges();

            var loan = new List<Loan>
            {
                
                new Loan{LoanID =1, BorrowerID =1, AssetID = 9, LoanDate = DateTime.Now, DueDate = DateTime.Now.AddDays(0)}
            };
            loan.ForEach(l => context.Loans.Add(l));
            context.SaveChanges();

            base.Seed(context);
            //string sqlscript = (context as IObjectContextAdapter).ObjectContext.CreateDatabaseScript();

            //public void getSQL()
            //{
            //}
        }
    }
}