
using ASP_MVC_Shop.Contexts;
using ASP_MVC_Shop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController 
    { 
        public ProductController() : base() { }
        // GET: Admin/Product
        //[Route("quan-ly-san-pham/{page:int?}")]
        public ActionResult Index(int page =1,int limit = 8,Boolean isTrash = false)
        {
            List<ViewProduct_0256> viewProducts = objEntity.ViewProduct_0256.Where(p => p.isTrash == isTrash).ToList();
            ViewBag.isTrash = isTrash;
            return View(viewProducts.ToPagedList(page, limit));
        }

        public ActionResult Detail(int id)
        {
            ViewProduct_0256 product = objEntity.ViewProduct_0256.Where(p => p.id == id).FirstOrDefault();
            return View(product);
        }
        private void getViewBagCreate()
        {
            ViewBag.listBrand = objEntity.Brand_0256.ToList();
            ViewBag.listCategory = objEntity.Category_0256.ToList();
            ViewBag.listType = objEntity.ProductType_0256.ToList();
        }
        public ActionResult Create()
        {
            getViewBagCreate();
            ViewBag.imageUrl = "";
            return View(new Product_0256());
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(Product_0256 product, String imageUrl)
        {
            getViewBagCreate();
            ViewBag.imageUrl = imageUrl;
            if (ModelState.IsValid)
            {          
                try
                {
                    if (imageUrl != null && !imageUrl.Equals(""))
                    {
                        Image_0256 image = new Image_0256() { url = imageUrl, isPublic = true, isTrash = false };
                        objEntity.Image_0256.Add(image);
                        objEntity.SaveChanges();
                        product.imageId = image.id;
                    }

                    product.isPublic = true;
                    product.isTrash = false;

                    objEntity.Product_0256.Add(product);
                    objEntity.SaveChanges();
                    lstToast.Add(new Toast() { message = "Thêm sản phẩm thành công!", type = "success" });
                    ViewBag.imageUrl = "";
                    EndAction();
                    return View(new Product_0256());                 
                }
                catch
                {
                    lstToast.Add(new Toast() { message = "Có lỗi khi thêm sản phẩm!", type = "error" });
                }
            }
            else
            {
                lstToast.Add(new Toast() { message = "Thêm sản phẩm không thành công!", type = "error" });
            }
            EndAction();
            return View(product);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = objEntity.Product_0256.Where(p=>p.id == id).FirstOrDefault();
            if(product == null)
            {
                
                lstToast.Add(new Toast() { message="Không tìm thấy sản phẩm",type="warning" });
                EndAction();
                return RedirectToAction("");
            }
            ViewBag.imageUrl = objEntity.Image_0256.FirstOrDefault(i => i.id == product.imageId).url;
            ViewBag.listBrand = objEntity.Brand_0256.ToList();
            ViewBag.listCategory = objEntity.Category_0256.ToList();
            ViewBag.listType = objEntity.ProductType_0256.ToList();
            return View(product);
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Update(Product_0256 product,String imageUrl)
        {
            getViewBagCreate();
            ViewBag.imageUrl = imageUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageUrl != null && !imageUrl.Equals(""))
                    {
                        Image_0256 image = new Image_0256() { url = imageUrl,isPublic = true,isTrash = false };
                        objEntity.Image_0256.Add(image);
                        objEntity.SaveChanges();

                        product.imageId = image.id;
                    }
                    objEntity.Entry(product).State = EntityState.Modified;
                    objEntity.SaveChanges();
                    lstToast.Add(new Toast() { message = "Cập nhật sản phẩm thành công!", type = "success" });
                }
                catch
                {
                    lstToast.Add(new Toast() { message = "Có lỗi khi cập nhật sản phẩm!", type = "error" });
                }
            }
            else
            {
                lstToast.Add(new Toast() { message = "Cập nhật sản phẩm không thành công!", type = "error" });
            }
            
            EndAction();
            return View(product);
        }
        public ActionResult Remove(int id)
        {
            
            try
            {
                Product_0256 product = objEntity.Product_0256.Where(p => p.id == id).FirstOrDefault();
                product.isTrash = true;
                objEntity.Entry(product).State = EntityState.Modified;
                objEntity.SaveChanges();
                return Json(new { data = true, toast = new Toast() { message = "Thêm sản phẩm vào thùng rác thành công!", type = "success" } });
            }
            catch
            {
                return Json(new { data = false, toast = new Toast() { message = "Thêm sản phẩm vào thùng rác không thành công!", type = "error" } });
            }
        }
        public ActionResult Restore(int id)
        {
            try
            {
                Product_0256 product = objEntity.Product_0256.Where(p => p.id == id).FirstOrDefault();
                product.isTrash = false;
                objEntity.Entry(product).State = EntityState.Modified;
                objEntity.SaveChanges();
                return Json(new { data = true,toast = new Toast() { message = "Khôi phục sản phẩm thành công!", type = "success" } }); ;
            }
            catch
            {
                return Json(new { data = false,toast = new Toast() { message = "Khôi phục sản phẩm không thành công!", type = "error" } });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Product_0256 product = objEntity.Product_0256.Where(p => p.id == id).FirstOrDefault();
                objEntity.Product_0256.Remove(product);
                objEntity.SaveChanges();
                return Json(new { data = true, toast = new Toast() { message = "Xóa sản phẩm thành công!", type = "success" } });
            }
            catch
            {
                return Json(new { data = false, toast = new Toast() { message = "Xóa sản phẩm không thành công!", type = "error" } });
            }
        }
    }
}