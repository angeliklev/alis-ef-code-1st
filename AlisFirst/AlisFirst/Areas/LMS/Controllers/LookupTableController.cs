using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.LMS.Controllers
{
    public class LookupTableController : Controller
    {
        //
        // GET: /LMS/LookupTable/
        CategoryRepository catRepo = new CategoryRepository();
        LocationRepository locRepo = new LocationRepository();
        SupplierRepository supRepo = new SupplierRepository();
        ManufacturerRepository manRepo = new ManufacturerRepository();
        AssetModelRepository assModRepo = new AssetModelRepository();
        AssetStatusRepository assStaRepo = new AssetStatusRepository();

        public ActionResult Index()
        {
            LookupModel viewModel = new LookupModel();
            IEnumerable<Category> listOfCats = catRepo.All;
            IEnumerable<Location> listOfLocs = locRepo.All;
            IEnumerable<Supplier> listOfSups = supRepo.All;
            IEnumerable<Manufacturer> listOfMans = manRepo.All;
            IEnumerable<AssetModel> listOfAssMods = assModRepo.All;
            IEnumerable<AssetStatus> listOfAssStas = assStaRepo.All;
            
            viewModel.lcvm =
                AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<ListCategoryViewModel>>(listOfCats);
            viewModel.llvm =
                AutoMapper.Mapper.Map<IEnumerable<Location>, IEnumerable<ListLocationViewModel>>(listOfLocs);
            viewModel.lsvm =
                AutoMapper.Mapper.Map<IEnumerable<Supplier>, IEnumerable<ListSupplierViewModel>>(listOfSups);
            viewModel.lmvm =
                AutoMapper.Mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ListManufacturerViewModel>>(listOfMans);
            viewModel.lamvm =
                AutoMapper.Mapper.Map<IEnumerable<AssetModel>, IEnumerable<ListAssetModelViewModel>>(listOfAssMods);
            viewModel.lasvm =
                AutoMapper.Mapper.Map<IEnumerable<AssetStatus>, IEnumerable<ListAssetStatusViewModel>>(listOfAssStas);
          
            return View(viewModel);
        }

    }
}
