using System.Web.Mvc;

namespace AlisFirst.Areas.AMS
{
    public class AMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "AMS_default",
                "AMS/{controller}/{action}/{id}",
                new { controller = "Dashboard" ,action = "Index", id = UrlParameter.Optional },
                new[] { "AlisFirst.Areas.AMS.Controllers" }
            );
        }
    }
}
