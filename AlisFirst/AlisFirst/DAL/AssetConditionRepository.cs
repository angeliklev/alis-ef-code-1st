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
    public class AssetConditionRepository : IAssetConditionRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssetCondition> All
        {
            get { return context.AssetConditions; }
        }

        public IQueryable<AssetCondition> AllIncluding(params Expression<Func<AssetCondition, object>>[] includeProperties)
        {
            IQueryable<AssetCondition> query = context.AssetConditions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssetCondition Find(int id)
        {
            return context.AssetConditions.Find(id);
        }

        public void InsertOrUpdate(AssetCondition assetcondition)
        {
            if (assetcondition.AssetConditionID == default(int)) {
                // New entity
                context.AssetConditions.Add(assetcondition);
            } else {
                // Existing entity
                context.Entry(assetcondition).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assetcondition = context.AssetConditions.Find(id);
            context.AssetConditions.Remove(assetcondition);
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

    public interface IAssetConditionRepository : IDisposable
    {
        IQueryable<AssetCondition> All { get; }
        IQueryable<AssetCondition> AllIncluding(params Expression<Func<AssetCondition, object>>[] includeProperties);
        AssetCondition Find(int id);
        void InsertOrUpdate(AssetCondition assetcondition);
        void Delete(int id);
        void Save();
    }
}