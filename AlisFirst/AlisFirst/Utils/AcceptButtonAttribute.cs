using System.Reflection;
using System.Web.Mvc;

namespace AlisFirst.Utils
{
    public class AcceptButtonAttribute : ActionMethodSelectorAttribute
    {
        public string ButtonName { get; set; }

        public AcceptButtonAttribute(string buttonName)
        {
            ButtonName = buttonName;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var req = controllerContext.RequestContext.HttpContext.Request;
            return !string.IsNullOrEmpty(req.Form[this.ButtonName]);
        }
    }
}