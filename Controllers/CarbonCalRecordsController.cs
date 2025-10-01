using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ESGArk.Models;

namespace ESGArk.Controllers
{
    public class CarbonCalRecordsController : Controller
    {
        private ESGArkEntities db = new ESGArkEntities();

        // GET: CarbonCalRecords
        public ActionResult LifeFootprint()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(db.CarbonCalRecords.ToList());
        }
        [HttpPost]
        public ActionResult LifeFootprint(CarbonCalRecord Model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                if (Model.BehaviorType == "交通" && Model.SubType == "開車" && Model.Unit == "公里" && Model.TotalCO2 > 1)
                {
                    msg = "你好";
                }
                else
                {
                    msg = "不好";
                }
            }
            ViewBag.msg = msg;
            return View();
        }
        // GET: CarbonCalRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarbonCalRecord carbonCalRecord = db.CarbonCalRecords.Find(id);
            if (carbonCalRecord == null)
            {
                return HttpNotFound();
            }
            return View(carbonCalRecord);
        }

        // GET: CarbonCalRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarbonCalRecords/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                int userId = int.Parse(form["UserId"]);
                string behaviorType = form["BehaviorType"];
                string subType = form["SubType"];
                double quantity = double.Parse(form["Quantity"]);
                string unit = form["Unit"];

                var calculator = new CarbonCalModule();
                var record = calculator.Calculate(userId, behaviorType, subType, quantity, unit);

                db.CarbonCalRecords.Add(record);
                db.SaveChanges();

                // 可選：記錄成功後導向結果頁或 index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "發生錯誤：" + ex.Message);
                return View("LifeFootprint");
            }
        }
        // GET: CarbonCalRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarbonCalRecord carbonCalRecord = db.CarbonCalRecords.Find(id);
            if (carbonCalRecord == null)
            {
                return HttpNotFound();
            }
            return View(carbonCalRecord);
        }

        // POST: CarbonCalRecords/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,BehaviorType,SubType,Quantity,Unit,CO2PerUnit,TotalCO2,CreatedAt")] CarbonCalRecord carbonCalRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carbonCalRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carbonCalRecord);
        }

        // GET: CarbonCalRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarbonCalRecord carbonCalRecord = db.CarbonCalRecords.Find(id);
            if (carbonCalRecord == null)
            {
                return HttpNotFound();
            }
            return View(carbonCalRecord);
        }

        // POST: CarbonCalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarbonCalRecord carbonCalRecord = db.CarbonCalRecords.Find(id);
            db.CarbonCalRecords.Remove(carbonCalRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
