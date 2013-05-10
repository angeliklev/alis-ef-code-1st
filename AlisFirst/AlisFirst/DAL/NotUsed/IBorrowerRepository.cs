using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlisFirst.Models;

namespace AlisFirst.DAL
{
    public interface IBorrowerRepository : IDisposable
    {
        IEnumerable<Borrower> GetBorrowers();
        IEnumerable<Borrower> FindByGivenName(string name);
        IEnumerable<Borrower> FindBySurname(string name);
        Borrower GetBorrowerByID(int borrowerID);
        Borrower GetBorrowerByEmployee(bool employee);
        void InsertBorrower(Borrower borrower);
        void DeleteBorrower(int borrowerID);
        void UpdateBorrower(Borrower borrower);
        void Save();
    }
}