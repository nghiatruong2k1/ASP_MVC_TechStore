using ASP_MVC_Shop.Contexts;
using ASP_MVC_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASP_MVC_Shop.Controllers
{
    public class ProfileController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["id"] == null)
            {
                this.lstToast.Add(new Toast() { message = "Vui lòng đăng nhập!", type = "warning" });
                EndAction();
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "user", action = "login", area = "" }
                    )
                );
            }
        }
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        [Route("don-hang")]
        public ActionResult Order()
        {
            if(Session["id"] != null)
            {
                int id = int.Parse(Session["id"].ToString());
                List<ProfileOrderModel> lstOrder = new List<ProfileOrderModel>();
                List<ViewOrder_0256> orders = objEntity.ViewOrder_0256.Where(o => o.userId == id).ToList();
                foreach(ViewOrder_0256 order in orders){
                    List<ViewOrderDetail_0256> products = objEntity.ViewOrderDetail_0256.Where(o => o.orderId == order.id).ToList();
                    lstOrder.Add(new ProfileOrderModel() { order = order,products = products });
                }
                return View(lstOrder);
            }
            else
            {
                return new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "user", action = "login", area = "" }
                    )
                );
            }
            
        }

        public ActionResult Seller()
        {
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }
    }
}