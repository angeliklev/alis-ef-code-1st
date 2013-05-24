using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListAssetModelViewModel
    {
        public int AssetModelID { get; set; }
        public string AssetModellName { get; set; }
    }
    public class EditAssetModelViewModel
    {
        public int AssetModelID { get; set; }
        public string AssetModelName { get; set; }
    }
    public class CreateAssetModelViewModel
    {
        public int AssetModelID { get; set; }
        public string AssetModelName { get; set; }
    }
    public class DeleteAssetModelViewModel
    {
        public int AssetModelID { get; set; }
        public string AssetModelName { get; set; }
    }
}