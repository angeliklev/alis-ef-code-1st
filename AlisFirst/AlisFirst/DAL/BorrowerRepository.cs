using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AlisFirst.Models
{ 
    public class BorrowerRepository : IBorrowerRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Borrower> All
        {
            get { return context.Borrowers; }
        }

        public IQueryable<Borrower> AllIncluding(params Expression<Func<Borrower, object>>[] includeProperties)
        {
            IQueryable<Borrower> query = context.Borrowers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Borrower Find(int id)
        {
            return context.Borrowers.Find(id);
        }

        public void InsertOrUpdate(Borrower borrower)
        {
            if (borrower.BorrowerID == default(int)) {
                // New entity
                context.Borrowers.Add(borrower);
            } else {
                // Existing entity
                context.Entry(borrower).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            context.Borrowers.Remove(borrower);
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

    public interface IBorrowerRepository : IDisposable
    {
        IQueryable<Borrower> All { get; }
        IQueryable<Borrower> AllIncluding(params Expression<Func<Borrower, object>>[] includeProperties);
        Borrower Find(int id);
        void InsertOrUpdate(Borrower borrower);
        void Delete(int id);
        void Save();
    }
}