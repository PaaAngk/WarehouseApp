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
    public class ListGoodsQuantController : Controller
    {   
        private MyDBEntities2 db = new MyDBEntities2(); 

        // GET: Goods
        public ViewResult Index()
        {
            return View();
        }

        // GET: Goods/Details/5
        public ActionResult Details(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (String.IsNullOrEmpty(searchString) && page == 1)
            {
                TempData["msg"] = "1";
                return View();
            }
            else
            {
                TempData["msg"] = "";
            }

            DateTime d1 = Convert.ToDateTime(searchString);
            

            //DateTime d2 = Convert.ToDateTime("5/1/2015");

            object[] parameters = {
                new SqlParameter("@date1",SqlDbType.DateTime) {Value=d1}
                //new SqlParameter("@date2",SqlDbType.DateTime) {Value=d2}
            };
            db.Database.CommandTimeout = 360;
            IEnumerable<listGoodsQuant_Result> query = db.Database.SqlQuery<listGoodsQuant_Result>("listGoodsQuant @date1", parameters).ToList();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
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
