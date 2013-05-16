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
    public class LocationRepository : ILocationRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Location> All
        {
            get { return context.Locations; }
        }

        public IQueryable<Location> AllIncluding(params Expression<Func<Location, object>>[] includeProperties)
        {
            IQueryable<Location> query = context.Locations;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Location Find(int id)
        {
            return context.Locations.Find(id);
        }

        public void InsertOrUpdate(Location location)
        {
            if (location.LocationID == default(int)) {
                // New entity
                context.Locations.Add(location);
            } else {
                // Existing entity
                context.Entry(location).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var location = context.Locations.Find(id);
            context.Locations.Remove(location);
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

    public interface ILocationRepository : IDisposable
    {
        IQueryable<Location> All { get; }
        IQueryable<Location> AllIncluding(params Expression<Func<Location, object>>[] includeProperties);
        Location Find(int id);
        void InsertOrUpdate(Location location);
        void Delete(int id);
        void Save();
    }
}