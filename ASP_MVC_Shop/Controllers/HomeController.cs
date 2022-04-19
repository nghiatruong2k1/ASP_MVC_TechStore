using ASP_MVC_Shop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int page = 1,int limit = 12)
        {

            HomeModel homeModel = new HomeModel();

            List<ViewBrand_0256> brands = objEntity.ViewBrand_0256.ToList();
            homeModel.listBrand = brands;

            List<ViewCategory_0256> categories = objEntity.ViewCategory_0256.ToList();

            homeModel.listCategory = categories;
            homeModel.listCategoryPopular = categories.Where(c => c.isPopular == true).ToList();


            List<ViewProduct_0256> products = objEntity.ViewProduct_0256.ToList();
            homeModel.listProductDeal = products.Where(p=>p.typeId == 1).ToPagedList(page, limit);
            homeModel.listProductRecommend = products.Where(p => p.typeId == 2).ToPagedList(page, limit);

            return View(homeModel);
        }
    
    }
}