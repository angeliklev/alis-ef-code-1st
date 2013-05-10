using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AlisFirst.Models
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
                new AssetModel { AssetModelID = 1, AssetModelName = "Elitebook 8470p",  ManufacturerID = 4, CategoryID = 1 },
                new AssetModel { AssetModelID = 2, AssetModelName = "Precision T1600",  ManufacturerID = 3, CategoryID = 2},
                new AssetModel { AssetModelID = 3, AssetModelName = "iPad 2 16GB WIFI", ManufacturerID = 1, CategoryID = 3 },
                new AssetModel { AssetModelID = 4, AssetModelName = "Precision T1500",  ManufacturerID = 3, CategoryID = 2 },
                new AssetModel { AssetModelID = 5, AssetModelName = "MV920",            ManufacturerID = 2, CategoryID = 4 },
                new AssetModel { AssetModelID = 6, AssetModelName = "GZ-HD7",           ManufacturerID = 5, CategoryID = 4 },
                new AssetModel { AssetModelID = 7, AssetModelName = "GZ-MG135",         ManufacturerID = 5, CategoryID = 4 },
                new AssetModel { AssetModelID = 8, AssetModelName = "Mini DV MV430i 200X Z0", ManufacturerID = 2, CategoryID = 4 },
                new AssetModel { AssetModelID = 9, AssetModelName = "NV-GN250GN 3CCD",   ManufacturerID = 6, CategoryID = 4 },
                new AssetModel { AssetModelID = 10, AssetModelName = "HC85E",            ManufacturerID = 7, CategoryID = 4 },
                new AssetModel { AssetModelID = 11, AssetModelName = "DSR-200AP",        ManufacturerID = 7, CategoryID = 4 },
                new AssetModel { AssetModelID = 12, AssetModelName = "Digital Ixus 60",  ManufacturerID = 2, CategoryID = 5 },
                new AssetModel { AssetModelID = 13, AssetModelName = "Ixus 70",          ManufacturerID = 2, CategoryID = 5 },
                new AssetModel { AssetModelID = 14, AssetModelName = "Ixus 75",          ManufacturerID = 2, CategoryID = 5 },
                new AssetModel { AssetModelID = 15, AssetModelName = "Ixus 80",          ManufacturerID = 2, CategoryID = 5 },
                new AssetModel { AssetModelID = 16, AssetModelName = " PowerShot A310",  ManufacturerID = 2, CategoryID = 5 }
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
                    AssetModelID = 4
                },
                new Asset 
                {
                    BarCode = "ALVA-0023", SerialNum = "504262101728", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 5
                },
                new Asset 
                {
                    BarCode = "ALVA-0024", SerialNum = "504262122638", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 5
                },
                new Asset 
                {
                    BarCode = "ALVA-0027", SerialNum = "11250691", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0028", SerialNum = "9251112", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0029", SerialNum = "9251474", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 6
                },
                new Asset 
                {
                    BarCode = "ALVA-0030", SerialNum = "112G1880", IsLoanable = true,
                    SupplierID = 3, AssetModelID = 7
                }
            };
            asset.ForEach(a => context.Assets.Add(a));
            context.SaveChanges();

            var borrower = new List<Borrower>
            {
                new Borrower 
                {
                    
                    BorrowerID = 0,
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
                    BorrowerID = 1,
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
                    BorrowerID = 2,
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

            base.Seed(context);
            //string sqlscript = (context as IObjectContextAdapter).ObjectContext.CreateDatabaseScript();

            //public void getSQL()
            //{

            //}
        }
    }
}