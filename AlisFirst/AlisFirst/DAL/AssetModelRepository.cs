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
    public class AssetModelRepository : IAssetModelRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssetModel> All
        {
            get { return context.AssetModels; }
        }

        public IQueryable<AssetModel> AllIncluding(params Expression<Func<AssetModel, object>>[] includeProperties)
        {
            IQueryable<AssetModel> query = context.AssetModels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssetModel Find(int id)
        {
            return context.AssetModels.Find(id);
        }

        public void InsertOrUpdate(AssetModel assetmodel)
        {
            if (assetmodel.AssetModelID == default(int)) {
                // New entity
                context.AssetModels.Add(assetmodel);
            } else {
                // Existing entity
                context.Entry(assetmodel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assetmodel = context.AssetModels.Find(id);
            context.AssetModels.Remove(assetmodel);
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

    public interface IAssetModelRepository : IDisposable
    {
        IQueryable<AssetModel> All { get; }
        IQueryable<AssetModel> AllIncluding(params Expression<Func<AssetModel, object>>[] includeProperties);
        AssetModel Find(int id);
        void InsertOrUpdate(AssetModel assetmodel);
        void Delete(int id);
        void Save();
    }
}