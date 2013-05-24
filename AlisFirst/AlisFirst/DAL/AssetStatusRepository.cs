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
    public class AssetStatusRepository : IAssetStatusRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssetStatus> All
        {
            get { return context.AssetStatuses; }
        }

        public IQueryable<AssetStatus> AllIncluding(params Expression<Func<AssetStatus, object>>[] includeProperties)
        {
            IQueryable<AssetStatus> query = context.AssetStatuses;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssetStatus Find(int id)
        {
            return context.AssetStatuses.Find(id);
        }

        public void InsertOrUpdate(AssetStatus assetstatus)
        {
            if (assetstatus.AssetStatusID == default(int)) {
                // New entity
                context.AssetStatuses.Add(assetstatus);
            } else {
                // Existing entity
                context.Entry(assetstatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assetstatus = context.AssetStatuses.Find(id);
            context.AssetStatuses.Remove(assetstatus);
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

    public interface IAssetStatusRepository : IDisposable
    {
        IQueryable<AssetStatus> All { get; }
        IQueryable<AssetStatus> AllIncluding(params Expression<Func<AssetStatus, object>>[] includeProperties);
        AssetStatus Find(int id);
        void InsertOrUpdate(AssetStatus assetstatus);
        void Delete(int id);
        void Save();
    }
}