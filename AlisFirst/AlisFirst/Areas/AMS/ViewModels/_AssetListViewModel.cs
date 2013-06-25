using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class _AssetListViewModel
    {
        public IEnumerable<int> SelectedLocations {get; set;}        
        public MvcPaging.IPagedList<AlisFirst.Models.Asset> Assets {get; set;}
    }
}