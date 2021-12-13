using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WarehouseApp.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace WarehouseApp.Views
{
    public class ContentGoodsWaybillsController : Controller
    {
        private MyDBEntities2 db = new MyDBEntities2();

        // GET: ContentGoodsWaybills


        public ViewResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            var ContentGoodsWaybills = from s in db.ContentGoodsWaybill select s;

            ContentGoodsWaybills = ContentGoodsWaybills.OrderBy(s => s.idContentGoodsWaybill);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ContentGoodsWaybills.ToPagedList(pageNumber, pageSize));
        }

        // GET: ContentGoodsWaybills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsWaybill contentGoodsWaybill = db.ContentGoodsWaybill.Find(id);
            if (contentGoodsWaybill == null)
            {
                return HttpNotFound();
            }
            return View(contentGoodsWaybill);
        }

        // GET: ContentGoodsWaybills/Create
        public ActionResult Create()
        {
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName");
            ViewBag.Waybill_WaybillNumber = new SelectList(db.Waybill, "WaybillNumber", "WaybillNumber");
            return View();
        }

        // POST: ContentGoodsWaybills/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idContentGoodsWaybill,Waybill_WaybillNumber,Goods_GoodsName,QuantityGoods")] ContentGoodsWaybill contentGoodsWaybill)
        {
            if (ModelState.IsValid)
            {
                try { 
                db.ContentGoodsWaybill.Add(contentGoodsWaybill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
            }
        }

            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsWaybill.Goods_GoodsName);
            ViewBag.Waybill_WaybillNumber = new SelectList(db.Waybill, "WaybillNumber", "WaybillNumber", contentGoodsWaybill.Waybill_WaybillNumber);
            return View(contentGoodsWaybill);
        }

        // GET: ContentGoodsWaybills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsWaybill contentGoodsWaybill = db.ContentGoodsWaybill.Find(id);
            if (contentGoodsWaybill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsWaybill.Goods_GoodsName);
            ViewBag.Waybill_WaybillNumber = new SelectList(db.Waybill, "WaybillNumber", "WaybillNumber", contentGoodsWaybill.Waybill_WaybillNumber);
            return View(contentGoodsWaybill);
        }

        // POST: ContentGoodsWaybills/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idContentGoodsWaybill,Waybill_WaybillNumber,Goods_GoodsName,QuantityGoods")] ContentGoodsWaybill contentGoodsWaybill)
        {
            if (ModelState.IsValid)
            {
                try { 
                db.Entry(contentGoodsWaybill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
            }
        }
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsWaybill.Goods_GoodsName);
            ViewBag.Waybill_WaybillNumber = new SelectList(db.Waybill, "WaybillNumber", "WaybillNumber", contentGoodsWaybill.Waybill_WaybillNumber);
            return View(contentGoodsWaybill);
        }

        // GET: ContentGoodsWaybills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsWaybill contentGoodsWaybill = db.ContentGoodsWaybill.Find(id);
            if (contentGoodsWaybill == null)
            {
                return HttpNotFound();
            }
            return View(contentGoodsWaybill);
        }

        // POST: ContentGoodsWaybills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            ContentGoodsWaybill contentGoodsWaybill = db.ContentGoodsWaybill.Find(id);
            db.ContentGoodsWaybill.Remove(contentGoodsWaybill);
            db.SaveChanges();
        }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
                }

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
