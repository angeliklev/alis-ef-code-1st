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
using AlisFirst.Areas.LMS.ViewModels;
using AlisFirst.Areas.AMS.ViewModels;

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


            //Mapping information for viewmodel/model conversion (discuss where to move this junk to, it certainly shouldn't be in here)
            AutoMapper.Mapper.CreateMap<Borrower, CreateBorrowerViewModel>();
            AutoMapper.Mapper.CreateMap<CreateBorrowerViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, EditBorrowerViewModel>();
            AutoMapper.Mapper.CreateMap<EditBorrowerViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, DeleteBorrowerViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteBorrowerViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, ListBorrowerViewModel>();
            AutoMapper.Mapper.CreateMap<ListBorrowerViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<ListEmployeeViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, ListEmployeeViewModel>();
            AutoMapper.Mapper.CreateMap<CreateEmployeeViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, CreateEmployeeViewModel>();
            AutoMapper.Mapper.CreateMap<EditEmployeeViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, EditEmployeeViewModel>();
            AutoMapper.Mapper.CreateMap<DeleteEmployeeViewModel, Borrower>();
            AutoMapper.Mapper.CreateMap<Borrower, DeleteEmployeeViewModel>();

            AutoMapper.Mapper.CreateMap<CreateLoanViewModel,Loan>();
            AutoMapper.Mapper.CreateMap<Loan, CreateLoanViewModel>();
            AutoMapper.Mapper.CreateMap<EditLoanViewModel, Loan>();
            AutoMapper.Mapper.CreateMap<Loan, EditLoanViewModel>();
        }
    }
}