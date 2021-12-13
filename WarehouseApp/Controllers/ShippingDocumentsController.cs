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
    public class ShippingDocumentsController : Controller
    {
        private MyDBEntities2 db = new MyDBEntities2();

        // GET: ShippingDocuments
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

            var shippingDocument = db.ShippingDocument.Include(s => s.Warehouse);
            //var waybill = from s in db.Waybill select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                //waybill = waybill.Where(s => s.Warehouse.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "number_desc":
                    shippingDocument = shippingDocument.OrderByDescending(s => s.ShippingDocumentNumber);
                    break;
                case "Date_desc":
                    shippingDocument = shippingDocument.OrderBy(s => s.UnloadingDate);
                    break;
                case "Date":
                    shippingDocument = shippingDocument.OrderByDescending(s => s.UnloadingDate);
                    break;
                default:
                    shippingDocument = shippingDocument.OrderBy(s => s.ShippingDocumentNumber);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(shippingDocument.ToPagedList(pageNumber, pageSize));
            //return View(waybill.ToList());
        }

        // GET: ShippingDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocument shippingDocument = db.ShippingDocument.Find(id);
            if (shippingDocument == null)
            {
                return HttpNotFound();
            }
            return View(shippingDocument);
        }

        // GET: ShippingDocuments/Create
        public ActionResult Create()
        {
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper");
            return View();
        }

        // POST: ShippingDocuments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingDocumentNumber,Warehouse_WarehouseNumber,UnloadingDate")] ShippingDocument shippingDocument)
        {
            if (ModelState.IsValid)
            {
                try { 
                db.ShippingDocument.Add(shippingDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
            }
        }

            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", shippingDocument.Warehouse_WarehouseNumber);
            return View(shippingDocument);
        }

        // GET: ShippingDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocument shippingDocument = db.ShippingDocument.Find(id);
            if (shippingDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", shippingDocument.Warehouse_WarehouseNumber);
            return View(shippingDocument);
        }

        // POST: ShippingDocuments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingDocumentNumber,Warehouse_WarehouseNumber,UnloadingDate")] ShippingDocument shippingDocument)
        {
            if (ModelState.IsValid)
            {
                try { 
                    db.Entry(shippingDocument).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
                }
            }
            ViewBag.Warehouse_WarehouseNumber = new SelectList(db.Warehouse, "WarehouseNumber", "Storekeeper", shippingDocument.Warehouse_WarehouseNumber);
            return View(shippingDocument);
        }

        // GET: ShippingDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingDocument shippingDocument = db.ShippingDocument.Find(id);
            if (shippingDocument == null)
            {
                return HttpNotFound();
            }
            return View(shippingDocument);
        }

        // POST: ShippingDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
                ShippingDocument shippingDocument = db.ShippingDocument.Find(id);
                db.ShippingDocument.Remove(shippingDocument);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ShippingDocument shippingDocument = db.ShippingDocument.Find(id);
                TempData["msg"] = ex.InnerException.InnerException.Message;
                return View(shippingDocument);
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
