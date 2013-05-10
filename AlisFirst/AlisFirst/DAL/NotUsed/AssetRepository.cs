using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AlisFirst.Models;

namespace AlisFirst.DAL
{
    public class AssetRepository : IAssetRepository, IDisposable
    {
        private AlisFirstContext _context;

        public AssetRepository(AlisFirstContext context)
        {
            _context = context;
        }

        public IEnumerable<Asset> GetAssets()
        {
            return _context.Assets.ToList();
        }

        public IEnumerable<Asset> FindByModel(string name)
        {
            return _context.Assets.Where(c => c.AssetModel.AssetModelName.StartsWith(name))
                .AsEnumerable<Asset>();
        }

        public IEnumerable<Asset> FindByManufacturer(string name)
        {
            return _context.Assets.Where(c => c.AssetModel.Manufacturer.ManufacturerName.StartsWith(name))
                .AsEnumerable<Asset>();
        }

        public IEnumerable<Asset> FindBySupplier(string name)
        {
            return _context.Assets.Where(c => c.Supplier.SupplierName.StartsWith(name))
                .AsEnumerable<Asset>();
        }

        public Asset GetAssetByID(int id)
        {
            return _context.Assets.Find(id);
        }

        public Asset GetAssetByModel(int id)
        {
            return _context.Assets.Where(c => c.AssetModelID == id).Single(); ;
        }

        public Asset GetAssetByManufacturer(int id)
        {
            return _context.Assets.Where(c => c.AssetModel.ManufacturerID == id).Single();
        }

        public Asset GetAssetBySupplier(int id)
        {
            return _context.Assets.Where(c => c.SupplierID == id).Single();
        }

        public Asset GetAssetByLoanable(bool loanable)
        {
            return _context.Assets.Where(c => c.IsLoanable == loanable).Single();
        }

        public Asset GetAssetByActive(bool active)
        {
            return _context.Assets.Where(c => c.IsActive == active).Single();
        }

        public void InsertAsset(Asset asset)
        {
            _context.Assets.Add(asset);
        }

        public void DeleteAsset(int assetID)
        {
            Asset asset = _context.Assets.Find(assetID);
            _context.Assets.Remove(asset);
        }

        public void UpdateAsset(Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}