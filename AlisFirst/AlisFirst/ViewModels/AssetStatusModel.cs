using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListAssetStatusViewModel
    {
        public int AssetStatusID { get; set; }
        public string AssetStatusName { get; set; }
    }
    public class EditAssetStatusViewModel
    {
        public int AssetStatusID { get; set; }
        public string AssetStatusName { get; set; }
    }
    public class CreateAssetStatusViewModel
    {
        public int AssetStatusID { get; set; }
        public string AssetStatusName { get; set; }
    }
    public class DeleteAssetStatusViewModel
    {
        public int AssetStatusID { get; set; }
        public string AssetStatusName { get; set; }
    }
}