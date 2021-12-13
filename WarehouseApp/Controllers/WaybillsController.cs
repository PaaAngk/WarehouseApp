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
using System.Data.SqlClient;

namespace WarehouseApp.Views
{
    public class WaybillsController : Controller
    {
        private MyDBEntities2 db = new MyDBEntities2();

        // GET: Waybills
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var waybill = db.Waybill.Include(w => w.Warehouse);
            //var waybill = from s in db.Waybill select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                //waybill = waybill.Where(s => s.Warehouse.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "number_desc":
                    waybill = waybill.OrderByDescending(s => s.WaybillNumber);
                    break;
                case "Date_desc":
                    waybill = waybill.OrderBy(s => s.EntertDate);
                    break;
                case "Date":
                    waybill = waybill.OrderByDescending(s => s.EntertDate);
                    break;
                default:
                    waybill = waybill.OrderBy(s => s.WaybillNumber);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(waybill.ToPagedList(pageNumber, pageSize));
            //return View(waybill.ToList());
        }


        // GET: Waybills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waybill waybill = db.Waybill.Find(id);
            if (waybill == null)
            {
                return HttpNotFound();
            }
            return View(waybill);
        }

        // GET: Waybills/Create
        public ActionResult Create()
        {
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper");
            return View();
        }

        // POST: Waybills/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WaybillNumber,Warehouse_WarehouseNumber,EntertDate")] Waybill waybill)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.Waybill.Add(waybill);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.InnerException.InnerException.Message;
                    ModelState.AddModelError("","Error: "+ sqlException);           
                }
                /*catch (DbUpdateException ex )
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator." + ex);
                }*/
            }

            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", waybill.Warehouse_WarehouseNumber);
            return View(waybill);
        }

        // GET: Waybills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waybill waybill = db.Waybill.Find(id);
            if (waybill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", waybill.Warehouse_WarehouseNumber);
            return View(waybill);
        }

        // POST: Waybills/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WaybillNumber,Warehouse_WarehouseNumber,EntertDate")] Waybill waybill)
        {
            if (ModelState.IsValid)
            {
                try { 
                db.Entry(waybill).State = EntityState.Modified;
                db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", waybill.Warehouse_WarehouseNumber);
            return View(waybill);
        }

        // GET: Waybills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waybill waybill = db.Waybill.Find(id);
            if (waybill == null)
            {
                return HttpNotFound();
            }
            return View(waybill);
        }

        // POST: Waybills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Waybill waybill = db.Waybill.Find(id);
                db.Waybill.Remove(waybill);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Waybill waybill = db.Waybill.Find(id);
                TempData["msg"] = ex.InnerException.InnerException.Message;
                return View(waybill);
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
