using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ModelBindController :BaseController
    {
        // GET: ModelBind  資料繫結練習
        public ActionResult Index()
        {
            var data = "hello world";
            ViewData.Model = data;
            return View();
          //  return View(data); 有可能會導致字串的 View

        }
        public ActionResult ViewBagDemo()
        {
          
            ViewBag.Text = "我是包包";  // == ViewData["Text"]

            return View();
        }
    }
}