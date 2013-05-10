using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AlisFirst.Models
{ 
    public class AssignedStatusRepository : IAssignedStatusRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<AssignedStatus> All
        {
            get { return context.AssignedStatus; }
        }

        public IQueryable<AssignedStatus> AllIncluding(params Expression<Func<AssignedStatus, object>>[] includeProperties)
        {
            IQueryable<AssignedStatus> query = context.AssignedStatus;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AssignedStatus Find(int id)
        {
            return context.AssignedStatus.Find(id);
        }

        public void InsertOrUpdate(AssignedStatus assignedstatus)
        {
            if (assignedstatus.AssignedStatusID == default(int)) {
                // New entity
                context.AssignedStatus.Add(assignedstatus);
            } else {
                // Existing entity
                context.Entry(assignedstatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var assignedstatus = context.AssignedStatus.Find(id);
            context.AssignedStatus.Remove(assignedstatus);
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

    public interface IAssignedStatusRepository : IDisposable
    {
        IQueryable<AssignedStatus> All { get; }
        IQueryable<AssignedStatus> AllIncluding(params Expression<Func<AssignedStatus, object>>[] includeProperties);
        AssignedStatus Find(int id);
        void InsertOrUpdate(AssignedStatus assignedstatus);
        void Delete(int id);
        void Save();
    }
}