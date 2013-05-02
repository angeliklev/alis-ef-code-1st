using System.Web.Mvc;

namespace AlisEF102.Areas.AMS
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
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
