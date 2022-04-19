using ASP_MVC_Shop.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            var viewCategorys = objEntity.ViewCategory_0256.ToList();
            return View(viewCategorys);
        }
        public ActionResult Delete(int id)
        {
            List<Toast> lstToast = TempData["toast"] as List<Toast>;
            if (lstToast == null)
            {
                lstToast = new List<Toast>();
            }
            try
            {
                Category_0256 category = objEntity.Category_0256.Where(b => b.id == id).FirstOrDefault();
                objEntity.Category_0256.Remove(category);
                objEntity.SaveChanges();

                lstToast.Add(new Toast() { message = "Xóa danh mục thành công!", type = "success" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Xóa thương hiêu không thành công!", type = "error" });
            }
            TempData["toast"] = lstToast;
            return RedirectToAction("/");

        }
        public ActionResult Detail(int id)
        {
            ViewCategory_0256 category = objEntity.ViewCategory_0256.Where(p => p.id == id).FirstOrDefault();
            return View(category);
        }
        public ActionResult Create()
        {
            return View(new Category_0256());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category_0256 category)
        {
            List<Toast> lstToast = TempData["toast"] as List<Toast>;
            if (lstToast == null)
            {
                lstToast = new List<Toast>();
            }
            try
            {
                objEntity.Category_0256.Add(category);
                objEntity.SaveChanges();
                lstToast.Add(new Toast() { message = "Thêm danh mục thành công!", type = "success" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Thêm danh mục không thành công!", type = "error" });
            }
            TempData["toast"] = lstToast;
            return RedirectToAction("create");
        }

        public ActionResult Update(int id)
        {
            Category_0256 category = objEntity.Category_0256.Where(p => p.id == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Update(Category_0256 category)
        {
            List<Toast> lstToast = TempData["toast"] as List<Toast>;
            if (lstToast == null)
            {
                lstToast = new List<Toast>();
            }
            try
            {
                objEntity.Entry(category).State = EntityState.Modified;
                objEntity.SaveChanges();
                lstToast.Add(new Toast() { message = "Sửa danh mục thành công!" });
            }
            catch
            {
                lstToast.Add(new Toast() { message = "Sửa danh mục không thành công!", type = "error" });
            }
            TempData["toast"] = lstToast;
            return RedirectToAction("update");
        }
    }
}