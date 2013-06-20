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
    public class AssignedLocationRepository : IAssignedLocationRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssignedLocation> All
        {
            get { return context.AssignedLocations; }
        }

        public IQueryable<AssignedLocation> AllIncluding(params Expression<Func<AssignedLocation, object>>[] includeProperties)
        {
            IQueryable<AssignedLocation> query = context.AssignedLocations;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssignedLocation Find(int id)
        {
            return context.AssignedLocations.Find(id);
        }

        public void InsertOrUpdate(AssignedLocation assignedlocation)
        {
            if (assignedlocation.AssignedLocationID == default(int)) {
                // New entity
                context.AssignedLocations.Add(assignedlocation);
            } else {
                // Existing entity
                context.Entry(assignedlocation).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assignedlocation = context.AssignedLocations.Find(id);
            context.AssignedLocations.Remove(assignedlocation);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public IEnumerable<AssignedLocation> GetLocHistory(int id)
        {
            IEnumerable<AssignedLocation> query = context.AssignedLocations
                .OrderBy(l => l.AssignedLocationDate)
                .Where(m => m.AssetID == id);
            return query;
        }
    }

    public interface IAssignedLocationRepository : IDisposable
    {
        IQueryable<AssignedLocation> All { get; }
        IQueryable<AssignedLocation> AllIncluding(params Expression<Func<AssignedLocation, object>>[] includeProperties);
        AssignedLocation Find(int id);
        void InsertOrUpdate(AssignedLocation assignedlocation);
        void Delete(int id);
        void Save();
        IEnumerable<AssignedLocation> GetLocHistory(int id);
    }
}