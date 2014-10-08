using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class DashboardViewModel
    {
        [Required]
        public String Barcode { get; set; }

    }
}