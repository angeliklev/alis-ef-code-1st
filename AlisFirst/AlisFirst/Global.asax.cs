using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AlisFirst.Models;
using AlisFirst.DAL;
using AlisFirst.ViewModels;

namespace AlisFirst
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()

        {
            Database.SetInitializer<AlisFirstContext>(new AlisFirstDBInitializer());

            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AutoMapper.Mapper.CreateMap<Category, ListCategoryViewModel>();
            AutoMapper.Mapper.CreateMap<ListCategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<Category, EditCategoryViewModel>();
            AutoMapper.Mapper.CreateMap<EditCategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<CreateCategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<Category, CreateCategoryViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteCategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<Category, DeleteCategoryViewModel>();
            AutoMapper.Mapper.CreateMap<Location, ListLocationViewModel>();
            AutoMapper.Mapper.CreateMap<ListLocationViewModel, Location>();
            AutoMapper.Mapper.CreateMap<Location, EditLocationViewModel>();
            AutoMapper.Mapper.CreateMap<EditLocationViewModel, Location>();
            AutoMapper.Mapper.CreateMap<CreateLocationViewModel, Location>();
            AutoMapper.Mapper.CreateMap<Location, CreateLocationViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteLocationViewModel, Location>();
            AutoMapper.Mapper.CreateMap<Location, DeleteLocationViewModel>();
            AutoMapper.Mapper.CreateMap<Supplier, ListSupplierViewModel>();
            AutoMapper.Mapper.CreateMap<ListSupplierViewModel, Supplier>();
            AutoMapper.Mapper.CreateMap<Supplier, EditSupplierViewModel>();
            AutoMapper.Mapper.CreateMap<EditSupplierViewModel, Supplier>();
            AutoMapper.Mapper.CreateMap<CreateSupplierViewModel, Supplier>();
            AutoMapper.Mapper.CreateMap<Supplier, CreateSupplierViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteSupplierViewModel, Supplier>();
            AutoMapper.Mapper.CreateMap<Supplier, DeleteSupplierViewModel>();
            AutoMapper.Mapper.CreateMap<Manufacturer, ListManufacturerViewModel>();
            AutoMapper.Mapper.CreateMap<ListManufacturerViewModel, Manufacturer>();
            AutoMapper.Mapper.CreateMap<Manufacturer, EditManufacturerViewModel>();
            AutoMapper.Mapper.CreateMap<EditManufacturerViewModel, Manufacturer>();
            AutoMapper.Mapper.CreateMap<CreateManufacturerViewModel, Manufacturer>();
            AutoMapper.Mapper.CreateMap<Manufacturer, CreateManufacturerViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteManufacturerViewModel, Manufacturer>();
            AutoMapper.Mapper.CreateMap<Manufacturer, DeleteManufacturerViewModel>();
            AutoMapper.Mapper.CreateMap<AssetModel, ListAssetModelViewModel>();
            AutoMapper.Mapper.CreateMap<ListAssetModelViewModel, AssetModel>();
            AutoMapper.Mapper.CreateMap<AssetModel, EditAssetModelViewModel>();
            AutoMapper.Mapper.CreateMap<EditAssetModelViewModel, AssetModel>();
            AutoMapper.Mapper.CreateMap<CreateAssetModelViewModel, AssetModel>();
            AutoMapper.Mapper.CreateMap<AssetModel, CreateAssetModelViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteAssetModelViewModel, AssetModel>();
            AutoMapper.Mapper.CreateMap<AssetModel, DeleteAssetModelViewModel>();
            AutoMapper.Mapper.CreateMap<AssetStatus, ListAssetStatusViewModel>();
            AutoMapper.Mapper.CreateMap<ListAssetStatusViewModel, AssetStatus>();
            AutoMapper.Mapper.CreateMap<AssetStatus, EditAssetStatusViewModel>();
            AutoMapper.Mapper.CreateMap<EditAssetStatusViewModel, AssetStatus>();
            AutoMapper.Mapper.CreateMap<CreateAssetStatusViewModel, AssetStatus>();
            AutoMapper.Mapper.CreateMap<AssetStatus, CreateAssetStatusViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteAssetStatusViewModel, AssetStatus>();
            AutoMapper.Mapper.CreateMap<AssetStatus, DeleteAssetStatusViewModel>();
        }
    }
}