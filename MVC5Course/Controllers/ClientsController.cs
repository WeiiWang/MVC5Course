using System.Collections.Generic;
using MVC5Course.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using MVC5Course.Models.ViewModel;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        //private FabricsEntities db = new FabricsEntities();
        ClientRepository repo;
        OccupationRepository occuRepo;

        public ClientsController()
        {
            repo = RepositoryHelper.GetClientRepository();
            occuRepo = RepositoryHelper.GetOccupationRepository(repo.UnitOfWork);
        }
        // GET: Clients
      
        public ActionResult Index(string keyword)
        {
            var client = repo.All();
            if (!string.IsNullOrEmpty(keyword))
            {
                return View(client.Where(p => p.FirstName.Contains(keyword)).ToList());
            }
            return View(client.Take(10).ToList());
        }
        [HttpPost]
        [HandleError(ExceptionType =typeof(DbEntityValidationException) ,View = "EFerror")]

        public ActionResult BatchUpdate(ClientBatchViewModel[] data)
        {
            //批次更新 不會用實體模型 來做驗證
            //所以要建立viewmodel 來使用
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var client = repo.Find(item.ClientId);
                    client.FirstName = item.FirstName;
                    client.MiddleName = item.MiddleName;
                    client.LastName = item.LastName;
                }

                //EF錯誤的偵錯方法 使用try catch 

                //try
                //{
                repo.UnitOfWork.Commit();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    List<string> errors = new List<string>();
                //    foreach (var vErros in ex.EntityValidationErrors)
                //    {
                //        foreach (var err in vErros.ValidationErrors)
                //        {
                //            errors.Add(err.PropertyName + ", " + err.ErrorMessage);

                //        }
                //    }
                //    return Content(string.Join(",", errors.ToArray()));

                //}

                return RedirectToAction("Index");

            }
            ViewData.Model = repo.All().Take(10);

            return View("Index");
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = repo.Find(id.Value);

            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            Client client=new Client();
            if (TryUpdateModel(client))
            {
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //if (ModelState.IsValid)
            //{
            //    repo.Add(client);
            //    repo.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = repo.Find(id.Value);

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var client = repo.Find(id);
            
            if (TryUpdateModel(client, "", null, new string[] { "FirstName" }))     //這個會產生modelstat 所以會回傳錯誤訊息
            {
                //這段範本問題很多  因為會更新有欄位  最好的驗證方式還是 viewmodel
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include =
        //    "ClientId,FirstName,MiddleName,LastName," +
        //    "Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City," +
        //    "ZipCode,Longitude,Latitude,Notes,IdNumber")] Client client)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //這段範本問題很多  因為會更新有欄位  最好的驗證方式還是 viewmodel
        //        var db = repo.UnitOfWork.Context;
        //        db.Entry(client).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
        //    return View(client);
        //}

        // GET: Clients/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var client = repo.Find(id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
        public ActionResult 建議少用(FormCollection form)
        {
            //FormClollection 不會產生 ModelState 建議少用
            form["abc"] = "cde";
            return Content("叫你不要用");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
