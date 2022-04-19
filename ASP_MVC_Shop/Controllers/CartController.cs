using ASP_MVC_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;
using System.Web.Routing;

namespace ASP_MVC_Shop.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        [Route("gio-hang")]
        public ActionResult Index()
        {
            List<CartModel> lstCart = new List<CartModel>();
            if(Session["cart"] != null)
            {
                lstCart = (List<CartModel>)Session["cart"];
            }
            return View(lstCart);
        }
        [HttpGet]
        public ActionResult GetData()
        {
            List<CartModel> lstCart = new List<CartModel>();
            if (Session["cart"] != null)
            {
                lstCart = (List<CartModel>)Session["cart"];
            }
            var jsonResult = Json(lstCart, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult Voucher(String voucher)
        {
            int voucherData = 0;
            Toast toast = new Toast();
            if (voucher!= null)
            {
                var voucherObj = objEntity.OrderVoucher_0256.FirstOrDefault(v => v.code.Equals(voucher));
                if (voucherObj != null)
                {
                    voucherData = voucherObj.value ?? 0;
                    toast.message = "Áp dụng mã giảm giá thành công !";
                    toast.type = "success";
                }
                else
                {
                    toast.message = "Mã giảm giá không hợp lệ !";
                    toast.type = "warning";
                }
            }
            return Json(new {data = voucherData,toast = toast }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            List<CartModel> lstCart = new List<CartModel>();
            Toast toast = new Toast();
            if (Session["cart"] == null)
            {
                var item = new CartModel(objEntity.ViewProduct_0256.Find(id), quantity = quantity);
                lstCart.Add(item);

                toast.message = "Thêm sản phẩm vào giỏ hàng thành công!";
                toast.type="success" ;
            }
            else
            {
                lstCart = (List<CartModel>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    lstCart[index].quantity += quantity;
                    toast.message = "Tăng số sản phẩm trong giỏ hàng thành công!";
                    toast.type = "success";
                }
                else
                {
                    lstCart.Add(new CartModel(objEntity.ViewProduct_0256.Find(id), quantity));
                    toast.message = "Thêm sản phẩm vào giỏ hàng thành công!";
                    toast.type = "success";
                }
            }
            Session["cart"] = lstCart;
            var jsonResult = Json(new { data= lstCart,toast = toast }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].id.Equals(id))
                    return i;
            return -1;
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            List<CartModel> lstCart = new List<CartModel>();
            if (Session["cart"] != null)
            {
                lstCart = (List<CartModel>)Session["cart"];
                int index = lstCart.FindIndex(c => c.id == id);
                lstCart.RemoveAt(index);
                Session["cart"] = lstCart;
            }

            var jsonResult = Json(new { data = lstCart, toast = new Toast() { message = "Xóa sản phẩm thành công", type = "success" } }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult Payment()
        {
            var jsonResult = new JsonResult();
            var lstCart = (List<CartModel>)Session["cart"];
            if (Session["id"] == null)
            {

                lstToast.Add(new Toast() { message = "Vui lòng đăng nhập để thanh toán!" });
                EndAction();
                return new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "user", action = "login", area = "" }
                    )
                );
            }
            else
            {
                try
                {                 
                    Order_0256 objOrder = new Order_0256();
                    objOrder.userId = int.Parse(Session["id"].ToString());
                    objOrder.createDate = DateTime.Now;
                    objEntity.Order_0256.Add(objOrder);
                    objEntity.SaveChanges();
                    int intOrderId = objOrder.id;
                    List<OrderDetail_0256> lstOrderDetail = new List<OrderDetail_0256>();
                    foreach (var item in lstCart)
                    {
                        OrderDetail_0256 obj = new OrderDetail_0256();
                        obj.quantity = item.quantity;
                        obj.orderId = intOrderId;
                        obj.productId = item.id;
                        if(item.salePrice != null && item.salePrice > 0)
                        {
                            obj.price = item.salePrice;
                        }
                        else
                        {
                            obj.price = item.price;
                        }

                        objEntity.OrderDetail_0256.Add(obj);
                        objEntity.SaveChanges();
                    }
                    Session.Remove("cart");
                    lstToast.Add(new Toast() { message = "Đặt hàng thành công", type = "success" });
                }
                catch
                {
                    lstToast.Add(new Toast() { message = "Đặt hàng không thành công", type = "error" });
                }
            }
            EndAction();
            return RedirectToAction("index");
            
        }
    }
}