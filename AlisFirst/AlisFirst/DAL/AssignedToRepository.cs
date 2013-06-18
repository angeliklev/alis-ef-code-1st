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
    public class AssignedToRepository : IAssignedToRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssignedTo> All
        {
            get { return context.AssignedToes; }
        }

        public IQueryable<AssignedTo> GetAssignedToByAssetID( int AssetID )
        {
            return All.Where( m => m.AssetID == AssetID );
        }

        public IQueryable<AssignedTo> AllIncluding(params Expression<Func<AssignedTo, object>>[] includeProperties)
        {
            IQueryable<AssignedTo> query = context.AssignedToes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssignedTo Find(int id)
        {
            return context.AssignedToes.Find(id);
        }

        public void InsertOrUpdate(AssignedTo assignedto)
        {
            if (assignedto.AssignedToID == default(int)) {
                // New entity
                context.AssignedToes.Add(assignedto);
            } else {
                // Existing entity
                context.Entry(assignedto).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assignedto = context.AssignedToes.Find(id);
            context.AssignedToes.Remove(assignedto);
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

    public interface IAssignedToRepository : IDisposable
    {
        IQueryable<AssignedTo> All { get; }
        IQueryable<AssignedTo> AllIncluding(params Expression<Func<AssignedTo, object>>[] includeProperties);
        AssignedTo Find(int id);
        void InsertOrUpdate(AssignedTo assignedto);
        void Delete(int id);
        void Save();
    }
}