using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;

namespace ASP_MVC_Shop.Controllers
{
    public class BrandController : BaseController
    {

        // GET: Category
        [Route("thuong-hieu")]
        public ActionResult index()
        {
            List<ViewBrand_0256> viewBrands = objEntity.ViewBrand_0256.ToList();
            return View(viewBrands);
        }
        public ActionResult Search(string query)
        {
            List<ViewBrand_0256> viewBrands = objEntity.ViewBrand_0256.Where(b => b.name.ToLower().Contains(query.ToLower())).ToList();
            ViewBag.query = query;
            return View(viewBrands);
        }
    }
}