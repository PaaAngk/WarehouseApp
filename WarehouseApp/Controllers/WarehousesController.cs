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
using System.Web.UI;

namespace WarehouseApp.Views
{
    public class WarehousesController : Controller
    {
        private MyDBEntities2 db = new MyDBEntities2();

        // GET: Warehouses
        public ViewResult Index(string sortOrder, string currentFilter, string CurrentFilter1, string SearchKeeperString, string searchString,  int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (SearchKeeperString != null || searchString != null)
            {
                page = 1;
            }
            else if (SearchKeeperString == null)
            {
                SearchKeeperString = currentFilter;
            }
            else if (searchString == null)
            {
                searchString = CurrentFilter1;
            }
            
            

            ViewBag.CurrentFilter = SearchKeeperString;
            ViewBag.CurrentFilter1 = searchString;

            var Warehouse = from s in db.Warehouse select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Warehouse = Warehouse.Where(s => s.WarehouseNumber.ToString() == searchString);
            }
            else if (!String.IsNullOrEmpty(SearchKeeperString))
            {
                Warehouse = Warehouse.Where(s => s.Storekeeper.Contains(SearchKeeperString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Warehouse = Warehouse.OrderByDescending(s => s.WarehouseNumber);
                    break;
                default:
                    Warehouse = Warehouse.OrderBy(s => s.WarehouseNumber);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Warehouse.ToPagedList(pageNumber, pageSize));

        }

        // GET: Warehouses/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarehouseNumber,Storekeeper")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                try
                {                              
                    db.Warehouse.Add(warehouse);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
                }
            }

            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarehouseNumber,Storekeeper")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Entry(warehouse).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.InnerException.InnerException.Message);
                }
        }
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.Warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            try { 
                Warehouse warehouse = db.Warehouse.Find(id);
                db.Warehouse.Remove(warehouse);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Warehouse warehouse = db.Warehouse.Find(id);
                TempData["msg"] = ex.InnerException.InnerException.Message;              
                return View(warehouse);
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
