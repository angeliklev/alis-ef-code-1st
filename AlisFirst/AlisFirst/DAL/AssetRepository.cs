using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AlisFirst.Models;

namespace AlisFirst.DAL
{ 
    public class AssetRepository : IAssetRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Asset> All
        {
            get { return context.Assets; }
        }

        public IQueryable<Asset> AllIncluding(params Expression<Func<Asset, object>>[] includeProperties)
        {
            IQueryable<Asset> query = context.Assets;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Asset Find(int id)
        {
            return context.Assets.Find(id);
        }

        public void InsertOrUpdate(Asset asset)
        {
            if (asset.AssetID == default(int)) {
                // New entity
                context.Assets.Add(asset);
            } else {
                // Existing entity
                context.Entry(asset).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var asset = context.Assets.Find(id);
            context.Assets.Remove(asset);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IAssetRepository : IDisposable
    {
        IQueryable<Asset> All { get; }
        IQueryable<Asset> AllIncluding(params Expression<Func<Asset, object>>[] includeProperties);
        Asset Find(int id);
        void InsertOrUpdate(Asset asset);
        void Delete(int id);
        void Save();
    }
}