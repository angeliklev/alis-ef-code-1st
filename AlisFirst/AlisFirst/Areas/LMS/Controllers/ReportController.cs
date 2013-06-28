using AlisFirst.Areas.LMS.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlisFirst.Areas.LMS.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILoanRepository loanRepository;
        public ReportController()
            : this(new LoanRepository())
        {
        }

        public ReportController(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }

        //
        //Get:/OverDueReport
        [HttpGet]
        public ViewResult Index()
        {
            IEnumerable<Loan> OverdueLoans = loanRepository.OverdueLoans;
            OverDueReport odr = new OverDueReport();
            odr.Overdues = AutoMapper.Mapper.Map<IEnumerable<Loan>, IEnumerable<EditLoanViewModel>>(loanRepository.OverdueLoans);
            return View(odr);
        }


        [HttpPost]
        public ActionResult Index(string SearchKey)
        {
            
            OverDueReport odr = new OverDueReport();

            if (ModelState.IsValid)
                if (String.IsNullOrEmpty(SearchKey))
                {
                    odr.Overdues = AutoMapper.Mapper.Map<IEnumerable<Loan>, IEnumerable<EditLoanViewModel>>(loanRepository.OverdueLoans);
                }
                else
                {
                    odr.Overdues = AutoMapper.Mapper.Map<IEnumerable<Loan>, IEnumerable<EditLoanViewModel>>(loanRepository.All.Where(ol => ol.Asset.BarCode == SearchKey && ol.DueDate < DateTime.Now));
                }
            return View(odr);
        }

        //[HttpPost]
        //public ActionResult Index(int id)
        //{
        //    return RedirectToAction("Edit", "Loans", new { LoanID = id });
        //}


    }
}
