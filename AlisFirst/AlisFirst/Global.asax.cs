﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using AlisFirst.Areas.LMS.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Helpers;
using AlisFirst.Models;


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
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "AlisFirst.Controllers" }
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

            AutoMapperBootstrapper.Initialize();

            //Mapping information for viewmodel/model conversion (discuss where to move this junk to, it certainly shouldn't be in here)
           
        }
    }
}