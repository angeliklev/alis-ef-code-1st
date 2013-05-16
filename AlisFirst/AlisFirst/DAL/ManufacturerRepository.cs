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
    public class ManufacturerRepository : IManufacturerRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Manufacturer> All
        {
            get { return context.Manufacturers; }
        }

        public IQueryable<Manufacturer> AllIncluding(params Expression<Func<Manufacturer, object>>[] includeProperties)
        {
            IQueryable<Manufacturer> query = context.Manufacturers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Manufacturer Find(int id)
        {
            return context.Manufacturers.Find(id);
        }

        public void InsertOrUpdate(Manufacturer manufacturer)
        {
            if (manufacturer.ManufacturerID == default(int)) {
                // New entity
                context.Manufacturers.Add(manufacturer);
            } else {
                // Existing entity
                context.Entry(manufacturer).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var manufacturer = context.Manufacturers.Find(id);
            context.Manufacturers.Remove(manufacturer);
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

    public interface IManufacturerRepository : IDisposable
    {
        IQueryable<Manufacturer> All { get; }
        IQueryable<Manufacturer> AllIncluding(params Expression<Func<Manufacturer, object>>[] includeProperties);
        Manufacturer Find(int id);
        void InsertOrUpdate(Manufacturer manufacturer);
        void Delete(int id);
        void Save();
    }
}