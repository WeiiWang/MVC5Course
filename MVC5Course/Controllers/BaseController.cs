using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    /// <summary>
    /// 修改base controller 找不到的會到首頁
    /// </summary>
    public abstract class BaseController : Controller   
        // 建議使用抽象類別 abstract 才不會被用戶找到
    {
        protected FabricsEntities _db = new FabricsEntities();
        protected override void HandleUnknownAction(string actionName)
        {
       
            this.RedirectToAction("index", "home",new {actname= actionName }).ExecuteResult(this.ControllerContext);
        }

    }
}