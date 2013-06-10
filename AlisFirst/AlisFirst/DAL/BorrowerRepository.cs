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
    public class BorrowerRepository : IBorrowerRepository
    {
        AlisFirstContext context;

        public BorrowerRepository()
        {
            context = new AlisFirstContext();
        }

        public BorrowerRepository(AlisFirstContext src_Context)
        {
            context = src_Context;
        }

        public IQueryable<Borrower> All
        {
            get { return context.Borrowers; }
        }

        public IQueryable<Loan> GetBorrowerLoans( int BorrowerID )
        {
            return context.Loans.Where(l => l.BorrowerID == BorrowerID);
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

        //Check if email is unique, this should use linq at some point, instead of program side iteration.
        public bool CheckEmailUnique( string srcEmail )
        {
            foreach( Borrower t in All )
            {
                if ( srcEmail == t.Email )
                {
                    return false;
                }
            }

            return true;
        }
        //Likewise as above, but check barcode instead.
        internal bool CheckBarcodeUnique(string src)
        {
            foreach (Borrower t in All)
            {
                if (src == t.BarCode)
                {
                    return false;
                }
            }
            return true;
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