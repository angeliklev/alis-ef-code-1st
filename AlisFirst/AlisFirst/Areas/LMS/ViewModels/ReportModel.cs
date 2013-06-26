using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.Areas.LMS.ViewModels
{
    public class ReportModel
    {
    }
     
    public class OverDueReport
    {
        public IEnumerable<EditLoanViewModel> Overdues;
        public string SearchKey = "";
    }
}