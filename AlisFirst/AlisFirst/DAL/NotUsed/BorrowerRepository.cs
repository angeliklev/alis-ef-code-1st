using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlisFirst.Models;
using AlisFirst.DAL;
using System.Data;

namespace AlisFirst.DAL
{
    public class BorrowerRepository : IBorrowerRepository, IDisposable
    {
        private AlisFirstContext _context;

        public BorrowerRepository(AlisFirstContext context)
        {
            _context = context;
        }

        public IEnumerable<Borrower> GetBorrowers()
        {
            return _context.Borrowers.ToList();
        }

        public IEnumerable<Borrower> FindByGivenName(string name)
        {
            return _context.Borrowers.Where(c => c.GivenName.StartsWith(name))
                .AsEnumerable<Borrower>();
        }

        public IEnumerable<Borrower> FindBySurname(string name)
        {
            return _context.Borrowers.Where(c => c.Surname.StartsWith(name))
                .AsEnumerable<Borrower>();
        }

        public Borrower GetBorrowerByID(int id)
        {
            return _context.Borrowers.Find(id);
        }

        public Borrower GetBorrowerByEmployee(bool employee)
        {
            return _context.Borrowers.Where(c => c.IsEmployee == employee).Single();
        }

        public void InsertBorrower(Borrower borrower)
        {
            _context.Borrowers.Add(borrower);
        }

        public void DeleteBorrower(int borrowerID)
        {
            Borrower borrower = _context.Borrowers.Find(borrowerID);
            _context.Borrowers.Remove(borrower);
        }

        public void UpdateBorrower(Borrower borrower)
        {
            _context.Entry(borrower).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}