using System.Web.Mvc;

namespace MVC5Course.Areas.th
{
    public class thAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "th";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "th_default",
                "th/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MVC5Course.Areas.th.Controllers" }
            );
        }
    }
}