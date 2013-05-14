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
    public class LoanRepository : ILoanRepository
    {
        AlisFirstContext context = new AlisFirstContext();

        public IQueryable<Loan> All
        {
            get { return context.Loans; }
        }

        public IQueryable<Loan> AllIncluding(params Expression<Func<Loan, object>>[] includeProperties)
        {
            IQueryable<Loan> query = context.Loans;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Loan Find(int id)
        {
            return context.Loans.Find(id);
        }

        public void InsertOrUpdate(Loan loan)
        {
            if (loan.LoanID == default(int)) {
                // New entity
                context.Loans.Add(loan);
            } else {
                // Existing entity
                context.Entry(loan).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var loan = context.Loans.Find(id);
            context.Loans.Remove(loan);
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

    public interface ILoanRepository : IDisposable
    {
        IQueryable<Loan> All { get; }
        IQueryable<Loan> AllIncluding(params Expression<Func<Loan, object>>[] includeProperties);
        Loan Find(int id);
        void InsertOrUpdate(Loan loan);
        void Delete(int id);
        void Save();
    }
}