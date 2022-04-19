using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        [Route("danh-muc")]
        public ActionResult index()
        {
            List<ViewCategory_0256> viewCategories = objEntity.ViewCategory_0256.ToList();
            return View(viewCategories);
        }
        public ActionResult Search(string query)
        {
            ViewBag.query = query;
            List<ViewCategory_0256> viewCategories = objEntity.ViewCategory_0256.Where(c=>c.name.ToLower().Contains(query.ToLower())).ToList();
            return View(viewCategories);
        }
    }
}