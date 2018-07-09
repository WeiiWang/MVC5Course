using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Viewtest()
        {
            var model = "罵罵號";
            return View((object)model);
        }
        public ActionResult partialviewtest()
        {
            var model = "罵罵號";
            return PartialView("Viewtest",(object)model);
        }
        public ActionResult Content()
        {

            return Content("ok");
        }
        public ActionResult FileTest(string dl)
        {
            if (string.IsNullOrEmpty(dl))
            {
                return File(Server.MapPath("~/App_Data/taiwan.png"), "image/png");
            }
            else
            {
                return File(Server.MapPath("~/App_Data/taiwan.png"), "image/png","taiwan_flag.png");
            }

        }


    }
}