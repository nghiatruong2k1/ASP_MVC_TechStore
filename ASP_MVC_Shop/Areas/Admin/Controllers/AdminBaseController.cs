using ASP_MVC_Shop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASP_MVC_Shop.Areas.Admin.Controllers
{
    public class AdminBaseController : BaseController
    {
        public AdminBaseController() : base()
        {

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            //if((Session["id"] == null) || (Session["typeId"] == null))
            //{
            //    this.lstToast.Add(new Toast() { message = "Vui lòng đăng nhập tài khoản quản trị", type = "warning" });
            //    EndAction();
            //    filterContext.Result = new RedirectToRouteResult(
            //        new RouteValueDictionary(
            //            new { controller = "user", action = "login", area = "" }
            //        )
            //    );
            //}
            //else
            //{
            //    if((Session["typeId"] != null && !Session["typeId"].Equals(4)))
            //    {
            //        this.lstToast.Add(new Toast() { message = "Bạn không có quyền truy cập trang quản trị", type = "warning" });
            //        EndAction();
            //        filterContext.Result = new RedirectToRouteResult(
            //            new RouteValueDictionary(
            //                new { controller = "", action = "", area = "" }
            //            )
            //        );
            //    }
            //}
        }
    }
}