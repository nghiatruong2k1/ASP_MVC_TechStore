using ASP_MVC_Shop.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop.Areas.Admin.Controllers
{
    public class BrandController : AdminBaseController
    {
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var viewBrands = objEntity.ViewBrand_0256.ToList();
            return View(viewBrands);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Brand_0256 brand = objEntity.Brand_0256.Where(b => b.id == id).FirstOrDefault();
                objEntity.Brand_0256.Remove(brand);
                objEntity.SaveChanges();

                lstToast.Add(new Toast() { message = "Xóa thương hiệu thành công!", type = "success" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Xóa thương hiêu không thành công!", type = "error" });
            }
            EndAction(); 
            return RedirectToAction("/");

        }
        public ActionResult Detail(int id)
        {
            ViewBrand_0256 brand = objEntity.ViewBrand_0256.Where(p => p.id == id).FirstOrDefault();
            return View(brand);
        }
        public ActionResult Create()
        {
            return View(new Brand_0256());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand_0256 brand)
        {
            try
            {
                objEntity.Brand_0256.Add(brand);
                objEntity.SaveChanges();
                lstToast.Add(new Toast() { message = "Thêm thương hiệu thành công!", type = "success" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Thêm thương hiệu không thành công!", type = "error" });
            }
            EndAction();
            return RedirectToAction("create");
        }

        public ActionResult Update(int id)
        {
            Brand_0256 brand = objEntity.Brand_0256.Where(p => p.id == id).FirstOrDefault();
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Update(Brand_0256 brand)
        {
            try
            {
                objEntity.Entry(brand).State = EntityState.Modified;
                objEntity.SaveChanges();
                lstToast.Add(new Toast() { message = "Sửa thương hiệu thành công!" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Sửa thương hiệu không thành công!", type = "error" });
            }
            EndAction();
            return RedirectToAction("update");
        }
    }
}