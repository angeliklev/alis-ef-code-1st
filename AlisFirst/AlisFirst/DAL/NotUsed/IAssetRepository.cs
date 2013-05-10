using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlisFirst.Models;

namespace AlisFirst.DAL
{
    public interface IAssetRepository : IDisposable
    {
        IEnumerable<Asset> GetAssets();
        IEnumerable<Asset> FindByModel(string name);
        IEnumerable<Asset> FindByManufacturer(string name);
        IEnumerable<Asset> FindBySupplier(string name);
        Asset GetAssetByID(int assetID);
        Asset GetAssetByModel(int assetID);
        Asset GetAssetByManufacturer(int assetID);
        Asset GetAssetBySupplier(int assetID);
        Asset GetAssetByLoanable(bool loanable);
        Asset GetAssetByActive(bool active);
        void InsertAsset(Asset asset);
        void DeleteAsset(int assetID);
        void UpdateAsset(Asset asset);
        void Save();
    }
}