using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AlisFirst.Models
{ 
    public class RepairRepository : IRepairRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Repair> All
        {
            get { return context.Repairs; }
        }

        public IQueryable<Repair> AllIncluding(params Expression<Func<Repair, object>>[] includeProperties)
        {
            IQueryable<Repair> query = context.Repairs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Repair Find(int id)
        {
            return context.Repairs.Find(id);
        }

        public void InsertOrUpdate(Repair repair)
        {
            if (repair.RepairID == default(int)) {
                // New entity
                context.Repairs.Add(repair);
            } else {
                // Existing entity
                context.Entry(repair).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var repair = context.Repairs.Find(id);
            context.Repairs.Remove(repair);
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

    public interface IRepairRepository : IDisposable
    {
        IQueryable<Repair> All { get; }
        IQueryable<Repair> AllIncluding(params Expression<Func<Repair, object>>[] includeProperties);
        Repair Find(int id);
        void InsertOrUpdate(Repair repair);
        void Delete(int id);
        void Save();
    }
}