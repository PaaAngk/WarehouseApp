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

namespace WarehouseApp.Views
{
    public class ContentGoodsShippingDocumentsController : Controller
    {
        private MyDBEntities2 db = new MyDBEntities2();

        // GET: ContentGoodsShippingDocuments
        public ViewResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            var ContentGoodsShippingDocuments = from s in db.ContentGoodsShippingDocument select s;

            ContentGoodsShippingDocuments = ContentGoodsShippingDocuments.OrderBy(s => s.idContentGoodsShippingDocument);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ContentGoodsShippingDocuments.ToPagedList(pageNumber, pageSize));
        }

        // GET: ContentGoodsShippingDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsShippingDocument contentGoodsShippingDocument = db.ContentGoodsShippingDocument.Find(id);
            if (contentGoodsShippingDocument == null)
            {
                return HttpNotFound();
            }
            return View(contentGoodsShippingDocument);
        }

        // GET: ContentGoodsShippingDocuments/Create
        public ActionResult Create()
        {
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName");
            ViewBag.ShippingDocument_ShippingDocumentNumber = new SelectList(db.ShippingDocument, "ShippingDocumentNumber", "ShippingDocumentNumber");
            return View();
        }

        // POST: ContentGoodsShippingDocuments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idContentGoodsShippingDocument,ShippingDocument_ShippingDocumentNumber,Goods_GoodsName,QuantityGoods")] ContentGoodsShippingDocument contentGoodsShippingDocument)
        {
            if (ModelState.IsValid)
            {
                db.ContentGoodsShippingDocument.Add(contentGoodsShippingDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsShippingDocument.Goods_GoodsName);
            ViewBag.ShippingDocument_ShippingDocumentNumber = new SelectList(db.ShippingDocument, "ShippingDocumentNumber", "ShippingDocumentNumber", contentGoodsShippingDocument.ShippingDocument_ShippingDocumentNumber);
            return View(contentGoodsShippingDocument);
        }

        // GET: ContentGoodsShippingDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsShippingDocument contentGoodsShippingDocument = db.ContentGoodsShippingDocument.Find(id);
            if (contentGoodsShippingDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsShippingDocument.Goods_GoodsName);
            ViewBag.ShippingDocument_ShippingDocumentNumber = new SelectList(db.ShippingDocument, "ShippingDocumentNumber", "ShippingDocumentNumber", contentGoodsShippingDocument.ShippingDocument_ShippingDocumentNumber);
            return View(contentGoodsShippingDocument);
        }

        // POST: ContentGoodsShippingDocuments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idContentGoodsShippingDocument,ShippingDocument_ShippingDocumentNumber,Goods_GoodsName,QuantityGoods")] ContentGoodsShippingDocument contentGoodsShippingDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contentGoodsShippingDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Goods_GoodsName = new SelectList(db.Goods, "GoodsName", "GoodsName", contentGoodsShippingDocument.Goods_GoodsName);
            ViewBag.ShippingDocument_ShippingDocumentNumber = new SelectList(db.ShippingDocument, "ShippingDocumentNumber", "ShippingDocumentNumber", contentGoodsShippingDocument.ShippingDocument_ShippingDocumentNumber);
            return View(contentGoodsShippingDocument);
        }

        // GET: ContentGoodsShippingDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentGoodsShippingDocument contentGoodsShippingDocument = db.ContentGoodsShippingDocument.Find(id);
            if (contentGoodsShippingDocument == null)
            {
                return HttpNotFound();
            }
            return View(contentGoodsShippingDocument);
        }

        // POST: ContentGoodsShippingDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContentGoodsShippingDocument contentGoodsShippingDocument = db.ContentGoodsShippingDocument.Find(id);
            db.ContentGoodsShippingDocument.Remove(contentGoodsShippingDocument);
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
