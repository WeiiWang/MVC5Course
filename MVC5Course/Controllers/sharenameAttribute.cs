using System;
using System.Web.Mvc;
using System.Web;
using System.Web.SessionState;

namespace MVC5Course.Controllers
{
    public class sharenameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.A = "123";
           
            base.OnActionExecuting(filterContext);
        }
    }
}