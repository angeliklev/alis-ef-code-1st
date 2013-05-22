using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetsViewModel
    {
        public IList<AlisFirst.Models.Asset> Assets { get; set; }

        public IList<AlisFirst.Models.AssetStatus> Status { get; set; }


    }
}