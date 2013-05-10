using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AlisFirst.Models;

namespace AlisFirst.Models
{ 
    public class SupplierRepository : ISupplierRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Supplier> All
        {
            get { return context.Suppliers; }
        }

        public IQueryable<Supplier> AllIncluding(params Expression<Func<Supplier, object>>[] includeProperties)
        {
            IQueryable<Supplier> query = context.Suppliers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Supplier Find(int id)
        {
            return context.Suppliers.Find(id);
        }

        public void InsertOrUpdate(Supplier supplier)
        {
            if (supplier.SupplierID == default(int)) {
                // New entity
                context.Suppliers.Add(supplier);
            } else {
                // Existing entity
                context.Entry(supplier).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var supplier = context.Suppliers.Find(id);
            context.Suppliers.Remove(supplier);
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

    public interface ISupplierRepository : IDisposable
    {
        IQueryable<Supplier> All { get; }
        IQueryable<Supplier> AllIncluding(params Expression<Func<Supplier, object>>[] includeProperties);
        Supplier Find(int id);
        void InsertOrUpdate(Supplier supplier);
        void Delete(int id);
        void Save();
    }
}