using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetsIndexViewModel
    {
        public IPagedList<AlisFirst.Models.Asset> Assets { get; set; }        

        public String searchKey { get; set; }

        public _AssetListViewModel listViewModel;



    }
}