using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlisFirst.Models;

namespace AlisFirst.ViewModels
{
    public class LookupModel
    {
        public IEnumerable<ListCategoryViewModel> lcvm;
        public IEnumerable<ListLocationViewModel> llvm;
        public IEnumerable<ListSupplierViewModel> lsvm;
        public IEnumerable<ListManufacturerViewModel> lmvm;
        public IEnumerable<ListAssetModelViewModel> lamvm;
        public IEnumerable<ListAssetStatusViewModel> lasvm;
    }
}