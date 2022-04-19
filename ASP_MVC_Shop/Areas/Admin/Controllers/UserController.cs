using ASP_MVC_Shop.Contexts;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page =1,int limit =100 ,Boolean isTrash = false)
        {
            List<ViewUser_0256> viewUsers = objEntity.ViewUser_0256.Where(p => p.isTrash == isTrash).ToList();
            ViewBag.isTrash = isTrash;
            return View(viewUsers.ToPagedList(page, limit));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Remove(int id)
        {
            try
            {
                User_0256 product = objEntity.User_0256.Where(p => p.id == id).FirstOrDefault();
                product.isTrash = true;
                objEntity.Entry(product).State = EntityState.Modified;
                objEntity.SaveChanges();
                return Json(new Toast() { message = "Thêm tài khoản vào thùng rác thành công!", type = "success" });
            }
            catch
            {
                return Json(new Toast() { message = "Thêm tài khoản vào thùng rác không thành công!", type = "error" });
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Restore(int id)
        {
            try
            {
                User_0256 product = objEntity.User_0256.Where(p => p.id == id).FirstOrDefault();
                product.isTrash = false;
                objEntity.Entry(product).State = EntityState.Modified;
                objEntity.SaveChanges();
                return Json(new Toast() { message = "Khôi phục tài khoản thành công!", type = "success" });
            }
            catch
            {
                return Json(new Toast() { message = "Khôi phục tài khoản không thành công!", type = "error" });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                User_0256 product = objEntity.User_0256.Where(p => p.id == id).FirstOrDefault();
                objEntity.User_0256.Remove(product);
                objEntity.SaveChanges();
                return Json(new Toast() { message = "Xóa tài khoản thành công!", type = "success" });
            }
            catch
            {
                return Json(new Toast() { message = "Xóa tài khoản không thành công!", type = "error" });
            }

        }
    }
}