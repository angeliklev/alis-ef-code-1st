using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.DAL
{ 
    public class LoanRepository : ILoanRepository
    {
        AlisFirstContext context = new AlisFirstContext();
        AssetConditionRepository assetConRepo = new AssetConditionRepository();

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

        //public IQueryable<Loan> AllOnLoans(params Expression<Func<Loan, object>>[] includeProperties)
        //{
        //    IQueryable<Loan> query = context.Loans.Where(ol => ol.ReturnDate == null);

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query();


        //}

        //public IList<Loan> AllOnLoans()
        //{
        //    var onLoans = from ol in context.Loans
        //                  where ol.ReturnDate == null
        //                  select All;

        //    return (Loan)onLoans.ToList();
        //}

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

        public int getBorrowerID(string BorrowerBarCode)
        {
            var borrowerID = from b in context.Borrowers
                             where b.BarCode == BorrowerBarCode
                             select b.BorrowerID;

            if (borrowerID.FirstOrDefault() != 0)
                return Convert.ToInt16(borrowerID.FirstOrDefault());

            return -1;
        }

        public int getAssetID(string AssetBarCode)
        { 
             var assetID = from a in context.Assets
                           where a.BarCode == AssetBarCode.Trim()
                           select a.AssetID;

            if(assetID.FirstOrDefault() != 0)
                return Convert.ToInt16(assetID.FirstOrDefault());
            return -1;
        }

        public Boolean IsOnLoan(int assetID)
        {
            var lastLoan = from ll in context.Loans
                           where ll.AssetID == assetID && ll.ReturnDate == null
                           select ll.LoanID;
            if (lastLoan.FirstOrDefault() != 0)
                return true;
            
            return false;            
        }

        public Boolean IsLoanable(int assetID)
        {
            var loanableAssetID = from a in context.Assets
                                  where a.AssetID == assetID && a.IsLoanable == true
                                  select a.AssetID;
            if (loanableAssetID.FirstOrDefault() !=0)
                return false;
            return true;
        }

        public DateTime getBorrowerExpiryDate(string borrowerBarcode)
        {
            int borrowerID = getBorrowerID(borrowerBarcode);

            var borrowerExpiryDate = from bed in context.Borrowers
                                     where bed.BorrowerID == borrowerID
                                     select bed.BorrowerExpiryDate;

            return (DateTime)borrowerExpiryDate.SingleOrDefault();
        }

        public IQueryable<Loan> AllOnLoans
        {
            get { return context.Loans.Where(ol => ol.ReturnDate == null); }
            
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
        int getBorrowerID(string BorrowerBarCode);
        int getAssetID(string AssetBarCode);
        Boolean IsOnLoan(int assetID);
        Boolean IsLoanable(int assetID);
        IQueryable<Loan> AllOnLoans{get;}

    }
}